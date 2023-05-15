use crate::model::{LinkType, LinksResult};
use doublets::data::{Flow, ReadHandler, WriteHandler};
use doublets::{
    data::{Flow::Continue, LinksConstants, ToQuery},
    mem::FileMapped,
    parts, split, Doublets, Error, Link, Links,
};
use smallvec::SmallVec;
use std::ops::Try;

pub struct Store<Inner>(Inner);

impl<Inner: Doublets<LinkType>> Store<Inner> {
    pub fn new(inner: Inner) -> Self {
        Self(inner)
    }

    pub fn iter(&self) -> impl Iterator<Item = Link<LinkType>> + '_ {
        self.0.iter()
    }

    pub fn each_iter<'a>(
        &self,
        query: impl ToQuery<LinkType> + 'a,
    ) -> impl Iterator<Item = Link<LinkType>> + 'a {
        self.0.each_iter(query)
    }
}

impl<Inner: Doublets<LinkType>> Links<LinkType> for Store<Inner> {
    fn constants(&self) -> &LinksConstants<LinkType> {
        self.0.constants()
    }

    fn count_links(&self, query: &[LinkType]) -> LinkType {
        self.0.count_links(query)
    }

    fn create_links(
        &mut self,
        query: &[LinkType],
        handler: WriteHandler<LinkType>,
    ) -> Result<Flow, Error<LinkType>> {
        self.0.create_links(query, handler)
    }

    fn each_links(&self, query: &[LinkType], handler: ReadHandler<LinkType>) -> Flow {
        self.0.each_links(query, handler)
    }

    fn update_links(
        &mut self,
        query: &[LinkType],
        change: &[LinkType],
        handler: WriteHandler<LinkType>,
    ) -> Result<Flow, Error<LinkType>> {
        self.0.update_links(query, change, handler)
    }

    fn delete_links(
        &mut self,
        query: &[LinkType],
        handler: WriteHandler<LinkType>,
    ) -> Result<Flow, Error<LinkType>> {
        self.0.delete_links(query, handler)
    }
}

impl<Inner: Doublets<LinkType>> Doublets<LinkType> for Store<Inner> {
    fn update_by_with<F, R>(
        &mut self,
        query: impl ToQuery<LinkType>,
        replacement: impl ToQuery<LinkType>,
        mut handler: F,
    ) -> Result<R, Error<LinkType>>
    where
        F: FnMut(Link<LinkType>, Link<LinkType>) -> R,
        R: Try<Output = ()>,
    {
        let query = query.to_query();
        let replacement = replacement.to_query();

        let constants = self.constants().clone();
        let store = &mut self.0;
        let (new, from_id, to_id) = (
            query[constants.index_part as usize],
            replacement[constants.source_part as usize],
            replacement[constants.target_part as usize],
        );
        let id = if let Some(old) = store.search(from_id, to_id) {
            store.rebase_with(new, old, &mut handler)?;
            store.delete_with(new, &mut handler)?;
            old
        } else {
            new
        };

        store.update_with(id, from_id, to_id, handler)
    }

    fn delete_by_with<F, R>(
        &mut self,
        query: impl ToQuery<LinkType>,
        mut handler: F,
    ) -> Result<R, Error<LinkType>>
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
