use crate::model::{LinkType, LinksResult};
use doublets::data::Flow::Continue;
use doublets::data::{LinksConstants, LinksError, ToQuery};
use doublets::{mem::FileMappedMem, splited, Doublets, Link};
use smallvec::SmallVec;
use std::ops::Try;

pub type RawStore = splited::Store<u64, FileMappedMem, FileMappedMem>;
type Inner = splited::Store<u64, FileMappedMem, FileMappedMem>;

pub struct Store(Inner);

impl Store {
    pub fn new(inner: Inner) -> Self {
        Self(inner)
    }

    pub fn iter(&self) -> impl Iterator<Item = Link<LinkType>> + '_ {
        // todo:
        //  self.0.iter()
        let mut vec = SmallVec::<[_; 2]>::new();

        self.try_each(|link| {
            vec.push(link);
            Continue
        });

        vec.into_iter()
    }

    pub fn each_iter<'a>(
        &self,
        query: impl ToQuery<LinkType> + 'a,
    ) -> impl Iterator<Item = Link<LinkType>> + 'a {
        self.0.each_iter(query)
    }

    pub fn update_iter<'a>(
        &mut self,
        id: LinkType,
        from_id: LinkType,
        to_id: LinkType,
    ) -> LinksResult<impl Iterator<Item = Link<LinkType>> + 'a> {
        let mut vec = SmallVec::<[_; 2]>::new();

        self.update_with(id, from_id, to_id, |before, link| {
            vec.push(link);
            Continue
        })?;

        Ok(vec.into_iter())
    }
}

impl Doublets<LinkType> for Store {
    fn constants(&self) -> LinksConstants<LinkType> {
        self.0.constants()
    }

    fn count_by(&self, query: impl ToQuery<LinkType>) -> LinkType {
        self.0.count_by(query)
    }

    fn create_by_with<F, R>(
        &mut self,
        query: impl ToQuery<LinkType>,
        handler: F,
    ) -> Result<R, LinksError<LinkType>>
    where
        F: FnMut(Link<LinkType>, Link<LinkType>) -> R,
        R: Try<Output = ()>,
    {
        self.0.create_by_with(query, handler)
    }

    fn try_each_by<F, R>(&self, restrictions: impl ToQuery<LinkType>, handler: F) -> R
    where
        F: FnMut(Link<LinkType>) -> R,
        R: Try<Output = ()>,
    {
        self.0.try_each_by(restrictions, handler)
    }

    fn update_by_with<F, R>(
        &mut self,
        query: impl ToQuery<LinkType>,
        replacement: impl ToQuery<LinkType>,
        mut handler: F,
    ) -> Result<R, LinksError<LinkType>>
    where
        F: FnMut(Link<LinkType>, Link<LinkType>) -> R,
        R: Try<Output = ()>,
    {
        let query = query.to_query();
        let replacement = replacement.to_query();

        let constants = self.constants();
        let store = &mut self.0;
        let (new, from_id, to_id) = (
            query[constants.index_part as usize],
            replacement[constants.source_part as usize],
            replacement[constants.target_part as usize],
        );
        let id = if let Some(old) = store.search(from_id, to_id) {
            store.rebase_with(old, new, &mut handler)?;
            store.delete_with(old, &mut handler)?;
            new
        } else {
            new
        };

        store.update_with(id, from_id, to_id, handler)
    }

    fn delete_by_with<F, R>(
        &mut self,
        query: impl ToQuery<LinkType>,
        mut handler: F,
    ) -> Result<R, LinksError<LinkType>>
    where
        F: FnMut(Link<LinkType>, Link<LinkType>) -> R,
        R: Try<Output = ()>,
    {
        let index_part = self.constants().index_part;
        let store = &mut self.0;
        store.delete_usages_with(query.to_query()[index_part as usize], &mut handler)?;
        store.delete_by_with(query, handler)
    }

    fn get_link(&self, index: LinkType) -> Option<Link<LinkType>> {
        self.0.get_link(index)
    }
}
