use crate::model::Numbers;
use async_graphql::*;

#[derive(Debug)]
pub struct NumbersMutationResponse;

#[Object(name = "numbers_mutation_response")]
impl NumbersMutationResponse {
    #[graphql(name = "affected_rows")]
    pub async fn affected_rows(&self, ctx: &Context<'_>) -> i32 {
        todo!()
    }
    pub async fn returning(&self, ctx: &Context<'_>) -> Vec<Numbers> {
        todo!()
    }
}
