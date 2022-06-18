use async_graphql::*;
#[derive(Enum, Debug, Copy, Clone, Eq, PartialEq)]
#[graphql(name = "numbers_update_column")]
pub enum NumbersUpdateColumn {
    #[graphql(name = "id")]
    Id,
    #[graphql(name = "link_id")]
    LinkId,
    #[graphql(name = "value")]
    Value,
}
