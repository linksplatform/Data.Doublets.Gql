use async_graphql::*;
#[derive(Enum, Debug, Copy, Clone, Eq, PartialEq)]
#[graphql(name = "numbers_select_column")]
pub enum NumbersSelectColumn {
    #[graphql(name = "id")]
    Id,
    #[graphql(name = "link_id")]
    LinkId,
    #[graphql(name = "value")]
    Value,
}
