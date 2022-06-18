use async_graphql::*;
#[derive(Debug)]
pub struct ObjectsVarSampFields;
#[Object(name = "objects_var_samp_fields")]
impl ObjectsVarSampFields {
    pub async fn id(&self, ctx: &Context<'_>) -> Option<f64> {
        todo!()
    }
    #[graphql(name = "link_id")]
    pub async fn link_id(&self, ctx: &Context<'_>) -> Option<f64> {
        todo!()
    }
}
