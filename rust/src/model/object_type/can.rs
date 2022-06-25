use crate::model::Bigint;
use crate::model::Links;
use async_graphql::*;

#[derive(Debug)]
pub struct Can;

#[Object(name = "can")]
impl Can {
    pub async fn action(&self, ctx: &Context<'_>) -> Option<Links> {
        todo!()
    }
    #[graphql(name = "action_id")]
    pub async fn action_id(&self, ctx: &Context<'_>) -> Option<Bigint> {
        todo!()
    }
    pub async fn object(&self, ctx: &Context<'_>) -> Option<Links> {
        todo!()
    }
    #[graphql(name = "object_id")]
    pub async fn object_id(&self, ctx: &Context<'_>) -> Option<Bigint> {
        todo!()
    }
    pub async fn rule(&self, ctx: &Context<'_>) -> Option<Links> {
        todo!()
    }
    #[graphql(name = "rule_id")]
    pub async fn rule_id(&self, ctx: &Context<'_>) -> Option<Bigint> {
        todo!()
    }
    pub async fn subject(&self, ctx: &Context<'_>) -> Option<Links> {
        todo!()
    }
    #[graphql(name = "subject_id")]
    pub async fn subject_id(&self, ctx: &Context<'_>) -> Option<Bigint> {
        todo!()
    }
}
