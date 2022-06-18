use async_graphql::*;
#[derive(Debug)]
pub struct NumbersStddevPopFields;
#[Object(name = "numbers_stddev_pop_fields")]
impl NumbersStddevPopFields {
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
