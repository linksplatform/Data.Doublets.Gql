use crate::model::Bigint;
use async_graphql::*;
#[derive(Debug)]
pub struct CanMinFields;
#[Object(name = "can_min_fields")]
impl CanMinFields {
    #[graphql(name = "action_id")]
    pub async fn action_id(&self, ctx: &Context<'_>) -> Option<Bigint> {
        todo!()
    }
    #[graphql(name = "object_id")]
    pub async fn object_id(&self, ctx: &Context<'_>) -> Option<Bigint> {
        todo!()
    }
    #[graphql(name = "rule_id")]
    pub async fn rule_id(&self, ctx: &Context<'_>) -> Option<Bigint> {
        todo!()
    }
    #[graphql(name = "subject_id")]
    pub async fn subject_id(&self, ctx: &Context<'_>) -> Option<Bigint> {
        todo!()
    }
}
