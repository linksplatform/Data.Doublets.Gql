use async_graphql::*;

#[derive(Debug)]
pub struct ObjectsStddevFields;

#[Object(name = "objects_stddev_fields")]
impl ObjectsStddevFields {
    pub async fn id(&self, ctx: &Context<'_>) -> Option<f64> {
        todo!()
    }
    #[graphql(name = "link_id")]
    pub async fn link_id(&self, ctx: &Context<'_>) -> Option<f64> {
        todo!()
    }
}
