//! Validate our pinned native file layout before passing pointers to Doublets.
use std::{
    collections::HashSet,
    fs::File,
    io::{self, BufReader, Read},
    path::Path,
};

// unit::Store reserves 2^20 records, including its header, and grows one slot
// early. Staying below that threshold avoids its large-file reopening bug.
pub(crate) const MAX_LINKS: usize = (1 << 20) - 2;
const RECORD_BYTES: usize = 64;
const FILE_BYTES: u64 = (1 << 20) * RECORD_BYTES as u64;
fn invalid() -> io::Error {
    io::Error::new(io::ErrorKind::InvalidData, "Unsupported or inconsistent db.links file; restore a valid server backup into a fresh directory")
}
fn record(reader: &mut impl Read) -> io::Result<[u64; 8]> {
    let mut bytes = [0; RECORD_BYTES];
    reader.read_exact(&mut bytes)?;
    Ok(std::array::from_fn(|i| {
        let mut word = [0; 8];
        word.copy_from_slice(&bytes[i * 8..(i + 1) * 8]);
        u64::from_ne_bytes(word)
    }))
}

pub(crate) fn validate(path: &Path) -> io::Result<()> {
    let file = match File::open(path) {
        Ok(file) => file,
        Err(e) if e.kind() == io::ErrorKind::NotFound => return Ok(()),
        Err(e) => return Err(e),
    };
    // A missing file means a new database; an existing partial/foreign file
    // must never be resized or silently initialized over its previous bytes.
    if file.metadata()?.len() != FILE_BYTES {
        return Err(invalid());
    }
    let mut reader = BufReader::new(file);
    let header = record(&mut reader)?;
    let allocated = header[0] as usize;
    if header[0] > MAX_LINKS as u64
        || header[1] != MAX_LINKS as u64 + 1
        || header[2] > header[0]
        || [3, 4, 5, 6].iter().any(|i| header[*i] > header[0])
    {
        return Err(invalid());
    }
    let mut rows = Vec::with_capacity(allocated + 1);
    rows.push(header);
    for _ in 0..allocated {
        rows.push(record(&mut reader)?);
    }
    // Native allocation reuses the reserved tail without clearing each record.
    // A stale pointer there must be rejected before a later insert can see it.
    let mut tail = [0u8; 64 * 1024];
    loop {
        let read = reader.read(&mut tail)?;
        if read == 0 {
            break;
        }
        if tail[..read].iter().any(|byte| *byte != 0) {
            return Err(invalid());
        }
    }
    let mut free = vec![false; rows.len()];
    let mut current = header[3] as usize;
    let mut previous = header[6] as usize;
    if header[2] == 0 && (current != 0 || previous != 0) {
        return Err(invalid());
    }
    for _ in 0..header[2] {
        if current == 0 || current > allocated || free[current] {
            return Err(invalid());
        }
        let row = rows[current];
        if row[0] != previous as u64 || row[1] > allocated as u64 || row[4] != 0 || row[7] != 0 {
            return Err(invalid());
        }
        free[current] = true;
        previous = current;
        current = row[1] as usize;
    }
    if current != header[3] as usize || previous != header[6] as usize {
        return Err(invalid());
    }
    let mut pairs = HashSet::new();
    for (i, row) in rows.iter().enumerate().skip(1) {
        if !free[i]
            && (row[0] > i64::MAX as u64
                || row[1] > i64::MAX as u64
                || !pairs.insert((row[0], row[1])))
        {
            return Err(invalid());
        }
        if row[2..].iter().any(|v| *v > allocated as u64) {
            return Err(invalid());
        }
    }
    validate_tree(&rows, &free, header[4] as usize, 0, 2)?;
    validate_tree(&rows, &free, header[5] as usize, 1, 5)?;
    Ok(())
}

fn validate_tree(
    rows: &[[u64; 8]],
    free: &[bool],
    root: usize,
    address: usize,
    left: usize,
) -> io::Result<()> {
    let mut visited = vec![false; rows.len()];
    let mut sizes = vec![0u64; rows.len()];
    let mut stack = vec![(root, None, None, false)];
    while let Some((id, lower, upper, returning)) = stack.pop() {
        if id == 0 {
            continue;
        }
        let row = rows[id];
        let key = (row[address], row[1 - address]);
        if returning {
            sizes[id] = 1 + sizes[row[left] as usize] + sizes[row[left + 1] as usize];
            if row[left + 2] != sizes[id] {
                return Err(invalid());
            }
            continue;
        }
        if visited[id]
            || free[id]
            || row[address] == 0
            || lower.is_some_and(|k| key <= k)
            || upper.is_some_and(|k| key >= k)
        {
            return Err(invalid());
        }
        visited[id] = true;
        stack.push((id, lower, upper, true));
        stack.push((row[left + 1] as usize, Some(key), upper, false));
        stack.push((row[left] as usize, lower, Some(key), false));
    }
    for (id, row) in rows.iter().enumerate().skip(1) {
        if visited[id] != (!free[id] && row[address] != 0) {
            return Err(invalid());
        }
        // attach() assumes inactive nodes have no children; a stale child here
        // could become reachable after an update or a deleted-slot reuse.
        if !visited[id] && row[left..left + 3].iter().any(|word| *word != 0) {
            return Err(invalid());
        }
    }
    Ok(())
}
