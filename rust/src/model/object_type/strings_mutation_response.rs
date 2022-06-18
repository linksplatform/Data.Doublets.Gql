use crate::model::Strings;
use async_graphql::*;
#[derive(Debug)]
pub struct StringsMutationResponse;
#[Object(name = "strings_mutation_response")]
impl StringsMutationResponse {
    #[graphql(name = "affected_rows")]
    pub async fn affected_rows(&self, ctx: &Context<'_>) -> i32 {
        todo!()
    }
    pub async fn returning(&self, ctx: &Context<'_>) -> Vec<Strings> {
        todo!()
    }
}
