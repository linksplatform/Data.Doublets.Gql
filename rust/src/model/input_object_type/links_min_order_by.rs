use crate::model::OrderBy;
use async_graphql::*;

#[derive(InputObject, Debug)]
#[graphql(name = "links_min_order_by")]
pub struct LinksMinOrderBy {
    #[graphql(name = "from_id")]
    pub from_id: Option<OrderBy>,
    pub id: Option<OrderBy>,
    #[graphql(name = "to_id")]
    pub to_id: Option<OrderBy>,
    #[graphql(name = "type_id")]
    pub type_id: Option<OrderBy>,
}
