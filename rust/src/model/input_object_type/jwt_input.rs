use async_graphql::*;
#[derive(InputObject, Debug)]
#[graphql(name = "JWTInput")]
pub struct JwtInput {
    pub link_id: Option<i32>,
}
