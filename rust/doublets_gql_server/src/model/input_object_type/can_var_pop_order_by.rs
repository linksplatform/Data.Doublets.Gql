use crate::model::OrderBy;
use async_graphql::*;
#[derive(InputObject, Debug)]
#[graphql(name = "can_var_pop_order_by")]
pub struct CanVarPopOrderBy {
    #[graphql(name = "action_id")]
    pub action_id: Option<OrderBy>,
    #[graphql(name = "object_id")]
    pub object_id: Option<OrderBy>,
    #[graphql(name = "rule_id")]
    pub rule_id: Option<OrderBy>,
    #[graphql(name = "subject_id")]
    pub subject_id: Option<OrderBy>,
}
