use crate::model::LinkType;
use async_graphql::*;
use std::cmp::Ordering;

#[derive(Enum, Debug, Copy, Clone, Eq, PartialEq)]
#[graphql(name = "order_by")]
pub enum OrderBy {
    #[graphql(name = "asc")]
    Asc,
    #[graphql(name = "asc_nulls_first")]
    AscNullsFirst,
    #[graphql(name = "asc_nulls_last")]
    AscNullsLast,
    #[graphql(name = "desc")]
    Desc,
    #[graphql(name = "desc_nulls_first")]
    DescNullsFirst,
    #[graphql(name = "desc_nulls_last")]
    DescNullsLast,
}

impl OrderBy {
    pub fn matches(&self, a: LinkType, b: LinkType) -> Ordering {
        match self {
            OrderBy::Asc => a.cmp(&b),
            OrderBy::AscNullsFirst => a.cmp(&b),
            OrderBy::AscNullsLast => b.cmp(&b),
            OrderBy::Desc => b.cmp(&a),
            OrderBy::DescNullsFirst => b.cmp(&a),
            OrderBy::DescNullsLast => b.cmp(&a),
        }
    }
}
