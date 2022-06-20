use crate::model::Bigint;
use async_graphql::*;

#[derive(Debug)]
pub struct SelectorsMinFields;

#[Object(name = "selectors_min_fields")]
impl SelectorsMinFields {
    #[graphql(name = "bool_exp_id")]
    pub async fn bool_exp_id(&self, ctx: &Context<'_>) -> Option<Bigint> {
        todo!()
    }
    #[graphql(name = "item_id")]
    pub async fn item_id(&self, ctx: &Context<'_>) -> Option<Bigint> {
        todo!()
    }
    #[graphql(name = "selector_id")]
    pub async fn selector_id(&self, ctx: &Context<'_>) -> Option<Bigint> {
        todo!()
    }
    #[graphql(name = "selector_include_id")]
    pub async fn selector_include_id(&self, ctx: &Context<'_>) -> Option<Bigint> {
        todo!()
    }
}
