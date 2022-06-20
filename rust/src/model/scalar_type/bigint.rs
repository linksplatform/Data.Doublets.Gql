use async_graphql::*;

pub type Bigint = i64;

pub trait LinksOptionExt {
    // todo: rename to into_link
    fn to_link(self) -> u64;
}

impl LinksOptionExt for Option<Bigint> {
    fn to_link(self) -> u64 {
        self.unwrap_or(0) as u64
    }
}
