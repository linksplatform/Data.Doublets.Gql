use async_graphql::*;
#[derive(Debug)]
pub struct StringsStddevSampFields;
#[Object(name = "strings_stddev_samp_fields")]
impl StringsStddevSampFields {
    pub async fn id(&self, ctx: &Context<'_>) -> Option<f64> {
        todo!()
    }
    #[graphql(name = "link_id")]
    pub async fn link_id(&self, ctx: &Context<'_>) -> Option<f64> {
        todo!()
    }
}
