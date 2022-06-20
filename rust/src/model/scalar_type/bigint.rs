use async_graphql::*;
#[derive(Debug, Clone)]
pub type Bigint = i64;

scalar!(Bigint, "bigint");
