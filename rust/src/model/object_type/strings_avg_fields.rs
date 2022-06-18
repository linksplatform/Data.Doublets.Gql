use async_graphql::*;
#[derive(Debug)]
pub struct StringsAvgFields;
#[Object(name = "strings_avg_fields")]
impl StringsAvgFields {
    pub async fn id(&self, ctx: &Context<'_>) -> Option<f64> {
        todo!()
    }
    #[graphql(name = "link_id")]
    pub async fn link_id(&self, ctx: &Context<'_>) -> Option<f64> {
        todo!()
    }
}
