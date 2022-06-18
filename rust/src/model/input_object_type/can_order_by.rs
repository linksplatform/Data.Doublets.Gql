use crate::model::LinksOrderBy;
use crate::model::OrderBy;
use async_graphql::*;
#[derive(InputObject, Debug)]
#[graphql(name = "can_order_by")]
pub struct CanOrderBy {
    pub action: Option<LinksOrderBy>,
    #[graphql(name = "action_id")]
    pub action_id: Option<OrderBy>,
    pub object: Option<LinksOrderBy>,
    #[graphql(name = "object_id")]
    pub object_id: Option<OrderBy>,
    pub rule: Option<LinksOrderBy>,
    #[graphql(name = "rule_id")]
    pub rule_id: Option<OrderBy>,
    pub subject: Option<LinksOrderBy>,
    #[graphql(name = "subject_id")]
    pub subject_id: Option<OrderBy>,
}
