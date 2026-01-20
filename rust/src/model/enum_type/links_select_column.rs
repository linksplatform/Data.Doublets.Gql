use async_graphql::*;

#[derive(Enum, Debug, Copy, Clone, Eq, PartialEq, Hash)]
#[graphql(name = "links_select_column")]
pub enum LinksSelectColumn {
    #[graphql(name = "from_id")]
    FromId,
    #[graphql(name = "id")]
    Id,
    #[graphql(name = "to_id")]
    ToId,
    #[graphql(name = "type_id")]
    TypeId,
}
