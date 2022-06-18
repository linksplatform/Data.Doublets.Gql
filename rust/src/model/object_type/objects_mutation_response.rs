use crate::model::Objects;
use async_graphql::*;
#[derive(Debug)]
pub struct ObjectsMutationResponse;
#[Object(name = "objects_mutation_response")]
impl ObjectsMutationResponse {
    #[graphql(name = "affected_rows")]
    pub async fn affected_rows(&self, ctx: &Context<'_>) -> i32 {
        todo!()
    }
    pub async fn returning(&self, ctx: &Context<'_>) -> Vec<Objects> {
        todo!()
    }
}
