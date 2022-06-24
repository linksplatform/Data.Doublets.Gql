use crate::model::Can;
use crate::model::CanAggregateFields;
use async_graphql::*;

#[derive(Debug)]
pub struct CanAggregate;

#[Object(name = "can_aggregate")]
impl CanAggregate {
    pub async fn aggregate(&self, ctx: &Context<'_>) -> Option<CanAggregateFields> {
        todo!()
    }
    pub async fn nodes(&self, ctx: &Context<'_>) -> Vec<Can> {
        todo!()
    }
}
