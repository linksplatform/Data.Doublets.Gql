use crate::model::Bigint;
use async_graphql::*;

#[derive(Debug)]
pub struct StringsSumFields;

#[Object(name = "strings_sum_fields")]
impl StringsSumFields {
    pub async fn id(&self, ctx: &Context<'_>) -> Option<Bigint> {
        todo!()
    }
    #[graphql(name = "link_id")]
    pub async fn link_id(&self, ctx: &Context<'_>) -> Option<Bigint> {
        todo!()
    }
}
