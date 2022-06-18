use async_graphql::*;
#[derive(Debug)]
pub struct CanVarSampFields;
#[Object(name = "can_var_samp_fields")]
impl CanVarSampFields {
    #[graphql(name = "action_id")]
    pub async fn action_id(&self, ctx: &Context<'_>) -> Option<f64> {
        todo!()
    }
    #[graphql(name = "object_id")]
    pub async fn object_id(&self, ctx: &Context<'_>) -> Option<f64> {
        todo!()
    }
    #[graphql(name = "rule_id")]
    pub async fn rule_id(&self, ctx: &Context<'_>) -> Option<f64> {
        todo!()
    }
    #[graphql(name = "subject_id")]
    pub async fn subject_id(&self, ctx: &Context<'_>) -> Option<f64> {
        todo!()
    }
}
