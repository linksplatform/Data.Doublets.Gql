use crate::model::Bigint;
use async_graphql::*;
#[derive(InputObject, Debug)]
#[graphql(name = "strings_inc_input")]
pub struct StringsIncInput {
    pub id: Option<Bigint>,
    #[graphql(name = "link_id")]
    pub link_id: Option<Bigint>,
}
