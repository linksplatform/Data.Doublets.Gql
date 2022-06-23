use crate::model::LinkType;
use async_graphql::*;

pub type Bigint = i64;

pub trait LinksOptionExt {
    // todo: rename to into_link
    fn to_link(self) -> LinkType;
}

impl LinksOptionExt for Option<Bigint> {
    fn to_link(self) -> LinkType {
        self.unwrap_or(0) as LinkType
    }
}
