use async_graphql::*;
#[derive(Debug)]
pub struct ObjectsAvgFields;
#[Object(name = "objects_avg_fields")]
impl ObjectsAvgFields {
    pub async fn id(&self, ctx: &Context<'_>) -> Option<f64> {
        todo!()
    }
    #[graphql(name = "link_id")]
    pub async fn link_id(&self, ctx: &Context<'_>) -> Option<f64> {
        todo!()
    }
}
