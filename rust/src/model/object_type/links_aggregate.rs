use crate::model::Links;
use crate::model::LinksAggregateFields;
use async_graphql::*;
#[derive(Debug)]
pub struct LinksAggregate;
#[Object(name = "links_aggregate")]
impl LinksAggregate {
    pub async fn aggregate(&self, ctx: &Context<'_>) -> Option<LinksAggregateFields> {
        todo!()
    }
    pub async fn nodes(&self, ctx: &Context<'_>) -> Vec<Links> {
        todo!()
    }
}
