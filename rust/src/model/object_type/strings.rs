use crate::model::Bigint;
use crate::model::Links;
use async_graphql::*;

#[derive(Debug)]
pub struct Strings;

#[Object(name = "strings")]
impl Strings {
    pub async fn id(&self, ctx: &Context<'_>) -> Bigint {
        todo!()
    }
    pub async fn link(&self, ctx: &Context<'_>) -> Option<Links> {
        todo!()
    }
    #[graphql(name = "link_id")]
    pub async fn link_id(&self, ctx: &Context<'_>) -> Option<Bigint> {
        todo!()
    }
    pub async fn value(&self, ctx: &Context<'_>) -> Option<String> {
        todo!()
    }
}
