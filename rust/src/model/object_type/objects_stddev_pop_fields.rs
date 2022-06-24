use async_graphql::*;

#[derive(Debug)]
pub struct ObjectsStddevPopFields;

#[Object(name = "objects_stddev_pop_fields")]
impl ObjectsStddevPopFields {
    pub async fn id(&self, ctx: &Context<'_>) -> Option<f64> {
        todo!()
    }
    #[graphql(name = "link_id")]
    pub async fn link_id(&self, ctx: &Context<'_>) -> Option<f64> {
        todo!()
    }
}
