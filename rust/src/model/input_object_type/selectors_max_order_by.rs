use crate::model::OrderBy;
use async_graphql::*;
#[derive(InputObject, Debug)]
#[graphql(name = "selectors_max_order_by")]
pub struct SelectorsMaxOrderBy {
    #[graphql(name = "bool_exp_id")]
    pub bool_exp_id: Option<OrderBy>,
    #[graphql(name = "item_id")]
    pub item_id: Option<OrderBy>,
    #[graphql(name = "selector_id")]
    pub selector_id: Option<OrderBy>,
    #[graphql(name = "selector_include_id")]
    pub selector_include_id: Option<OrderBy>,
}
