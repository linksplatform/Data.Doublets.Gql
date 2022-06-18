use crate::model::LinksAggregateOrderBy;
use crate::model::LinksOrderBy;
use crate::model::OrderBy;
use async_graphql::*;
#[derive(InputObject, Debug)]
#[graphql(name = "selectors_order_by")]
pub struct SelectorsOrderBy {
    #[graphql(name = "bool_exp_aggregate")]
    pub bool_exp_aggregate: Option<LinksAggregateOrderBy>,
    #[graphql(name = "bool_exp_id")]
    pub bool_exp_id: Option<OrderBy>,
    pub item: Option<LinksOrderBy>,
    #[graphql(name = "item_id")]
    pub item_id: Option<OrderBy>,
    pub selector: Option<LinksOrderBy>,
    #[graphql(name = "selector_id")]
    pub selector_id: Option<OrderBy>,
    #[graphql(name = "selector_include_id")]
    pub selector_include_id: Option<OrderBy>,
}
