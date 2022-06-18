use async_graphql::*;
#[derive(Debug)]
pub struct SelectorsAvgFields;
#[Object(name = "selectors_avg_fields")]
impl SelectorsAvgFields {
    #[graphql(name = "bool_exp_id")]
    pub async fn bool_exp_id(&self, ctx: &Context<'_>) -> Option<f64> {
        todo!()
    }
    #[graphql(name = "item_id")]
    pub async fn item_id(&self, ctx: &Context<'_>) -> Option<f64> {
        todo!()
    }
    #[graphql(name = "selector_id")]
    pub async fn selector_id(&self, ctx: &Context<'_>) -> Option<f64> {
        todo!()
    }
    #[graphql(name = "selector_include_id")]
    pub async fn selector_include_id(&self, ctx: &Context<'_>) -> Option<f64> {
        todo!()
    }
}
