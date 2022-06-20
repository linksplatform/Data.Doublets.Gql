use crate::model::Bigint;
use async_graphql::*;

#[derive(Debug)]
pub struct StringsMinFields;

#[Object(name = "strings_min_fields")]
impl StringsMinFields {
    pub async fn id(&self, ctx: &Context<'_>) -> Option<Bigint> {
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
