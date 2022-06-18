use async_graphql::*;
#[derive(Debug)]
pub struct GuestOutput;
#[Object]
impl GuestOutput {
    pub async fn link_id(&self, ctx: &Context<'_>) -> Option<i32> {
        todo!()
    }
    pub async fn token(&self, ctx: &Context<'_>) -> Option<String> {
        todo!()
    }
}
