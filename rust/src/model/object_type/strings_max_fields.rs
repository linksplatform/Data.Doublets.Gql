use crate::model::Bigint;
use async_graphql::*;

#[derive(Debug)]
pub struct StringsMaxFields;

#[Object(name = "strings_max_fields")]
impl StringsMaxFields {
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
