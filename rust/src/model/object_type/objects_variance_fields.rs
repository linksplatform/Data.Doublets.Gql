use async_graphql::*;

#[derive(Debug)]
pub struct ObjectsVarianceFields;

#[Object(name = "objects_variance_fields")]
impl ObjectsVarianceFields {
    pub async fn id(&self, ctx: &Context<'_>) -> Option<f64> {
        todo!()
    }
    #[graphql(name = "link_id")]
    pub async fn link_id(&self, ctx: &Context<'_>) -> Option<f64> {
        todo!()
    }
}
