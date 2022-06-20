use crate::model::Bigint;
use async_graphql::*;

#[derive(InputObject, Debug)]
#[graphql(name = "numbers_set_input")]
pub struct NumbersSetInput {
    pub id: Option<Bigint>,
    #[graphql(name = "link_id")]
    pub link_id: Option<Bigint>,
    pub value: Option<Bigint>,
}
