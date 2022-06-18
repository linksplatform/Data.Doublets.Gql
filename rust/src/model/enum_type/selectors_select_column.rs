use async_graphql::*;
#[derive(Enum, Debug, Copy, Clone, Eq, PartialEq)]
#[graphql(name = "selectors_select_column")]
pub enum SelectorsSelectColumn {
    #[graphql(name = "bool_exp_id")]
    BoolExpId,
    #[graphql(name = "item_id")]
    ItemId,
    #[graphql(name = "selector_id")]
    SelectorId,
    #[graphql(name = "selector_include_id")]
    SelectorIncludeId,
}
