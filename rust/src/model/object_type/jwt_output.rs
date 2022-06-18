use async_graphql::*;
#[derive(Debug)]
pub struct JwtOutput;
#[Object(name = "JWTOutput")]
impl JwtOutput {
    pub async fn error(&self, ctx: &Context<'_>) -> Option<String> {
        todo!()
    }
    pub async fn link_id(&self, ctx: &Context<'_>) -> Option<i32> {
        todo!()
    }
    pub async fn token(&self, ctx: &Context<'_>) -> Option<String> {
        todo!()
    }
}
