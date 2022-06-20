use crate::model::Links;
use async_graphql::*;

#[derive(Debug)]
pub struct LinksMutationResponse(pub Vec<Links>);

#[Object(name = "links_mutation_response")]
impl LinksMutationResponse {
    #[graphql(name = "affected_rows")]
    pub async fn affected_rows(&self, ctx: &Context<'_>) -> i32 {
        self.0.len() as i32
    }

    pub async fn returning(&self, ctx: &Context<'_>) -> Vec<Links> {
        self.0.clone()
    }
}
