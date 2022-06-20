use async_graphql::*;

#[derive(Debug)]
pub struct ObjectsStddevSampFields;

#[Object(name = "objects_stddev_samp_fields")]
impl ObjectsStddevSampFields {
    pub async fn id(&self, ctx: &Context<'_>) -> Option<f64> {
        todo!()
    }
    #[graphql(name = "link_id")]
    pub async fn link_id(&self, ctx: &Context<'_>) -> Option<f64> {
        todo!()
    }
}
