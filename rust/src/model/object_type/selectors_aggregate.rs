use crate::model::Selectors;
use crate::model::SelectorsAggregateFields;
use async_graphql::*;
#[derive(Debug)]
pub struct SelectorsAggregate;
#[Object(name = "selectors_aggregate")]
impl SelectorsAggregate {
    pub async fn aggregate(&self, ctx: &Context<'_>) -> Option<SelectorsAggregateFields> {
        todo!()
    }
    pub async fn nodes(&self, ctx: &Context<'_>) -> Vec<Selectors> {
        todo!()
    }
}
