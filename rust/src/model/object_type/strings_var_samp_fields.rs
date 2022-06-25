use async_graphql::*;

#[derive(Debug)]
pub struct StringsVarSampFields;

#[Object(name = "strings_var_samp_fields")]
impl StringsVarSampFields {
    pub async fn id(&self, ctx: &Context<'_>) -> Option<f64> {
        todo!()
    }
    #[graphql(name = "link_id")]
    pub async fn link_id(&self, ctx: &Context<'_>) -> Option<f64> {
        todo!()
    }
}
