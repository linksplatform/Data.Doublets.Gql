use async_graphql::*;

#[derive(Enum, Debug, Copy, Clone, Eq, PartialEq)]
#[graphql(name = "strings_select_column")]
pub enum StringsSelectColumn {
    #[graphql(name = "id")]
    Id,
    #[graphql(name = "link_id")]
    LinkId,
    #[graphql(name = "value")]
    Value,
}
