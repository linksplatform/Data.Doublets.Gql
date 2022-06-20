use crate::model::Bigint;
use async_graphql::*;

#[derive(Debug)]
pub struct ObjectsSumFields;

#[Object(name = "objects_sum_fields")]
impl ObjectsSumFields {
    pub async fn id(&self, ctx: &Context<'_>) -> Option<Bigint> {
        todo!()
    }
    #[graphql(name = "link_id")]
    pub async fn link_id(&self, ctx: &Context<'_>) -> Option<Bigint> {
        todo!()
    }
}
