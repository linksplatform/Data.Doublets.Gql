use doublets::{
    mem::{FileMapped, RawMem},
    parts::LinkPart,
    unit,
};
use std::mem::MaybeUninit;
use std::{
    fs::{self, File},
    io,
    path::Path,
    sync::{mpsc, Arc, Mutex},
    thread,
};
use tokio::sync::oneshot;

pub(crate) type RawStore = unit::Store<u64, StoreMemory<LinkPart<u64>>>;

/// Compatibility boundary for doublets 0.5's resize_mem and platform-mem 0.3.
/// Doublets expects the complete allocation after growth. FileMapped returns
/// only the new tail, and its default grow_filled overwrites existing file data.
/// Preserve mapped bytes and hand the native store the complete allocation.
pub(crate) struct StoreMemory<T>(FileMapped<T>);
impl<T> RawMem for StoreMemory<T> {
    type Item = T;
    fn allocated(&self) -> &[T] {
        self.0.allocated()
    }
    fn allocated_mut(&mut self) -> &mut [T] {
        self.0.allocated_mut()
    }
    unsafe fn grow(
        &mut self,
        addition: usize,
        fill: impl FnOnce(usize, (&mut [T], &mut [MaybeUninit<T>])),
    ) -> doublets::mem::Result<&mut [T]> {
        // SAFETY: forward the caller's initialization contract unchanged.
        unsafe {
            self.0.grow(addition, fill)?;
        }
        Ok(self.0.allocated_mut())
    }
    fn grow_filled(&mut self, addition: usize, value: T) -> doublets::mem::Result<&mut [T]>
    where
        T: Clone,
    {
        // SAFETY: FileMapped reports the already initialized elements read
        // from the file; grow_filled_exact initializes every remaining element.
        unsafe {
            self.0.grow_filled_exact(addition, value)?;
        }
        Ok(self.0.allocated_mut())
    }
    fn shrink(&mut self, amount: usize) -> doublets::mem::Result<()> {
        self.0.shrink(amount)
    }
}
type Job = Box<dyn FnOnce(&mut RawStore) + Send>;

struct Worker {
    sender: mpsc::Sender<Option<Job>>,
    handle: Mutex<Option<thread::JoinHandle<()>>>,
}

impl Drop for Worker {
    fn drop(&mut self) {
        let _ = self.sender.send(None);
        if let Ok(handle) = self.handle.get_mut() {
            if let Some(handle) = handle.take() {
                let _ = handle.join();
            }
        }
    }
}

/// Serializes access on the thread owning Doublets' non-Send memory mappings.
/// No unsafe Send/Sync implementations or locks held across GraphQL resolution.
#[derive(Clone)]
pub struct Database(Arc<Worker>);

impl Database {
    pub fn open(path: impl AsRef<Path>) -> io::Result<Self> {
        let path = path.as_ref().to_path_buf();
        let (sender, receiver) = mpsc::channel::<Option<Job>>();
        let (ready_tx, ready_rx) = mpsc::sync_channel(1);
        let handle = thread::Builder::new()
            .name("doublets-store".into())
            .spawn(move || {
                let open = || -> io::Result<(RawStore, File)> {
                    fs::create_dir_all(&path)?;
                    let lock = File::options()
                        .create(true)
                        .truncate(false)
                        .read(true)
                        .write(true)
                        .open(path.join("server.lock"))?;
                    lock.try_lock().map_err(io::Error::other)?;
                    crate::storage_format::validate(&path.join("db.links"))?;
                    let memory = StoreMemory(FileMapped::from_path(path.join("db.links"))?);
                    let store = RawStore::new(memory).map_err(io::Error::other)?;
                    Ok((store, lock))
                };
                match open() {
                    Ok((mut store, _lock)) => {
                        let _ = ready_tx.send(Ok(()));
                        while let Ok(Some(job)) = receiver.recv() {
                            job(&mut store);
                        }
                        // Dropping the store syncs the mapping before releasing the lock.
                        drop(store);
                    }
                    Err(error) => {
                        let _ = ready_tx.send(Err(error));
                    }
                }
            })?;
        match ready_rx.recv().map_err(io::Error::other)? {
            Ok(()) => Ok(Self(Arc::new(Worker {
                sender,
                handle: Mutex::new(Some(handle)),
            }))),
            Err(error) => {
                let _ = handle.join();
                Err(error)
            }
        }
    }

    pub(crate) async fn execute<T: Send + 'static>(
        &self,
        operation: impl FnOnce(&mut RawStore) -> Result<T, String> + Send + 'static,
    ) -> async_graphql::Result<T> {
        let (sender, receiver) = oneshot::channel();
        self.0
            .sender
            .send(Some(Box::new(move |store| {
                let _ = sender.send(operation(store));
            })))
            .map_err(|_| async_graphql::Error::new("Database worker is unavailable"))?;
        receiver
            .await
            .map_err(|_| async_graphql::Error::new("Database operation did not complete"))?
            .map_err(async_graphql::Error::new)
    }
}
