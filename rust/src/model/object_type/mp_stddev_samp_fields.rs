use async_graphql::*;

#[derive(Debug)]
pub struct MpStddevSampFields;

#[Object(name = "mp_stddev_samp_fields")]
impl MpStddevSampFields {
    #[graphql(name = "group_id")]
    pub async fn group_id(&self, ctx: &Context<'_>) -> Option<f64> {
        todo!()
    }
    pub async fn id(&self, ctx: &Context<'_>) -> Option<f64> {
        todo!()
    }
    #[graphql(name = "item_id")]
    pub async fn item_id(&self, ctx: &Context<'_>) -> Option<f64> {
        todo!()
    }
    #[graphql(name = "path_item_depth")]
    pub async fn path_item_depth(&self, ctx: &Context<'_>) -> Option<f64> {
        todo!()
    }
    #[graphql(name = "path_item_id")]
    pub async fn path_item_id(&self, ctx: &Context<'_>) -> Option<f64> {
        todo!()
    }
    #[graphql(name = "root_id")]
    pub async fn root_id(&self, ctx: &Context<'_>) -> Option<f64> {
        todo!()
    }
}
