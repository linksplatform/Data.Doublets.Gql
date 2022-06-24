use crate::model::Numbers;
use crate::model::NumbersAggregateFields;
use async_graphql::*;

#[derive(Debug)]
pub struct NumbersAggregate;

#[Object(name = "numbers_aggregate")]
impl NumbersAggregate {
    pub async fn aggregate(&self, ctx: &Context<'_>) -> Option<NumbersAggregateFields> {
        todo!()
    }
    pub async fn nodes(&self, ctx: &Context<'_>) -> Vec<Numbers> {
        todo!()
    }
}
