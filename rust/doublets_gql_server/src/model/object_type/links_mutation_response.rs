use crate::model::Links;
use async_graphql::*;
#[derive(Debug)]
pub struct LinksMutationResponse;
#[Object(name = "links_mutation_response")]
impl LinksMutationResponse {
    #[graphql(name = "affected_rows")]
    pub async fn affected_rows(&self, ctx: &Context<'_>) -> i32 {
        todo!()
    }
    pub async fn returning(&self, ctx: &Context<'_>) -> Vec<Links> {
        todo!()
    }
}
