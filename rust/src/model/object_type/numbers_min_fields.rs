use crate::model::Bigint;
use async_graphql::*;
#[derive(Debug)]
pub struct NumbersMinFields;
#[Object(name = "numbers_min_fields")]
impl NumbersMinFields {
    pub async fn id(&self, ctx: &Context<'_>) -> Option<Bigint> {
        todo!()
    }
    #[graphql(name = "link_id")]
    pub async fn link_id(&self, ctx: &Context<'_>) -> Option<Bigint> {
        todo!()
    }
    pub async fn value(&self, ctx: &Context<'_>) -> Option<Bigint> {
        todo!()
    }
}
