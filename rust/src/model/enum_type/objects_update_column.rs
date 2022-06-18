use async_graphql::*;
#[derive(Enum, Debug, Copy, Clone, Eq, PartialEq)]
#[graphql(name = "objects_update_column")]
pub enum ObjectsUpdateColumn {
    #[graphql(name = "id")]
    Id,
    #[graphql(name = "link_id")]
    LinkId,
    #[graphql(name = "value")]
    Value,
}
