use crate::model::LinksOrderBy;
use crate::model::OrderBy;
use async_graphql::*;
#[derive(InputObject, Debug)]
#[graphql(name = "strings_order_by")]
pub struct StringsOrderBy {
    pub id: Option<OrderBy>,
    pub link: Option<LinksOrderBy>,
    #[graphql(name = "link_id")]
    pub link_id: Option<OrderBy>,
    pub value: Option<OrderBy>,
}
