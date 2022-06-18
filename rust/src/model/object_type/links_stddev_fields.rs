use async_graphql::*;
#[derive(Debug)]
pub struct LinksStddevFields;
#[Object(name = "links_stddev_fields")]
impl LinksStddevFields {
    #[graphql(name = "from_id")]
    pub async fn from_id(&self, ctx: &Context<'_>) -> Option<f64> {
        todo!()
    }
    pub async fn id(&self, ctx: &Context<'_>) -> Option<f64> {
        todo!()
    }
    #[graphql(name = "to_id")]
    pub async fn to_id(&self, ctx: &Context<'_>) -> Option<f64> {
        todo!()
    }
    #[graphql(name = "type_id")]
    pub async fn type_id(&self, ctx: &Context<'_>) -> Option<f64> {
        todo!()
    }
}
