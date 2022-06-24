use async_graphql::*;

#[derive(Debug)]
pub struct NumbersStddevSampFields;

#[Object(name = "numbers_stddev_samp_fields")]
impl NumbersStddevSampFields {
    pub async fn id(&self, ctx: &Context<'_>) -> Option<f64> {
        todo!()
    }
    #[graphql(name = "link_id")]
    pub async fn link_id(&self, ctx: &Context<'_>) -> Option<f64> {
        todo!()
    }
    pub async fn value(&self, ctx: &Context<'_>) -> Option<f64> {
        todo!()
    }
}
