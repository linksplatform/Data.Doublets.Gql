use async_graphql::*;

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
