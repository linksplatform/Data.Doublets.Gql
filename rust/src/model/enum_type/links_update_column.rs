use async_graphql::*;

#[derive(Enum, Debug, Copy, Clone, Eq, PartialEq)]
#[graphql(name = "links_update_column")]
pub enum LinksUpdateColumn {
    #[graphql(name = "from_id")]
    FromId,
    #[graphql(name = "id")]
    Id,
    #[graphql(name = "to_id")]
    ToId,
    #[graphql(name = "type_id")]
    TypeId,
}
