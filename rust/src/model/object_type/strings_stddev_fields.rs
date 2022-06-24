use async_graphql::*;

#[derive(Debug)]
pub struct StringsStddevFields;

#[Object(name = "strings_stddev_fields")]
impl StringsStddevFields {
    pub async fn id(&self, ctx: &Context<'_>) -> Option<f64> {
        todo!()
    }
    #[graphql(name = "link_id")]
    pub async fn link_id(&self, ctx: &Context<'_>) -> Option<f64> {
        todo!()
    }
}
