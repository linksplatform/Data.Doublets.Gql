use crate::model::LinksOrderBy;
use crate::model::OrderBy;
use async_graphql::*;

#[derive(InputObject, Debug)]
#[graphql(name = "numbers_order_by")]
pub struct NumbersOrderBy {
    pub id: Option<OrderBy>,
    pub link: Option<LinksOrderBy>,
    #[graphql(name = "link_id")]
    pub link_id: Option<OrderBy>,
    pub value: Option<OrderBy>,
}
