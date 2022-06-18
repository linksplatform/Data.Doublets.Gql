use crate::model::Objects;
use crate::model::ObjectsAggregateFields;
use async_graphql::*;
#[derive(Debug)]
pub struct ObjectsAggregate;
#[Object(name = "objects_aggregate")]
impl ObjectsAggregate {
    pub async fn aggregate(&self, ctx: &Context<'_>) -> Option<ObjectsAggregateFields> {
        todo!()
    }
    pub async fn nodes(&self, ctx: &Context<'_>) -> Vec<Objects> {
        todo!()
    }
}
