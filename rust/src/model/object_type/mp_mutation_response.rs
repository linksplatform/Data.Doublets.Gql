use crate::model::Mp;
use async_graphql::*;

#[derive(Debug)]
pub struct MpMutationResponse;

#[Object(name = "mp_mutation_response")]
impl MpMutationResponse {
    #[graphql(name = "affected_rows")]
    pub async fn affected_rows(&self, ctx: &Context<'_>) -> i32 {
        todo!()
    }
    pub async fn returning(&self, ctx: &Context<'_>) -> Vec<Mp> {
        todo!()
    }
}
