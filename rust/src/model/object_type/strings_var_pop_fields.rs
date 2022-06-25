use async_graphql::*;

#[derive(Debug)]
pub struct StringsVarPopFields;

#[Object(name = "strings_var_pop_fields")]
impl StringsVarPopFields {
    pub async fn id(&self, ctx: &Context<'_>) -> Option<f64> {
        todo!()
    }
    #[graphql(name = "link_id")]
    pub async fn link_id(&self, ctx: &Context<'_>) -> Option<f64> {
        todo!()
    }
}
