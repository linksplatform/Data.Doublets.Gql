use crate::model::Mp;
use crate::model::MpAggregateFields;
use async_graphql::*;

#[derive(Debug)]
pub struct MpAggregate;

#[Object(name = "mp_aggregate")]
impl MpAggregate {
    pub async fn aggregate(&self, ctx: &Context<'_>) -> Option<MpAggregateFields> {
        todo!()
    }
    pub async fn nodes(&self, ctx: &Context<'_>) -> Vec<Mp> {
        todo!()
    }
}
