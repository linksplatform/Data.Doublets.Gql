use async_graphql::*;

#[derive(Enum, Debug, Copy, Clone, Eq, PartialEq)]
#[graphql(name = "objects_select_column")]
pub enum ObjectsSelectColumn {
    #[graphql(name = "id")]
    Id,
    #[graphql(name = "link_id")]
    LinkId,
    #[graphql(name = "value")]
    Value,
}
