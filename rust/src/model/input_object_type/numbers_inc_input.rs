use crate::model::Bigint;
use async_graphql::*;

#[derive(InputObject, Debug)]
#[graphql(name = "numbers_inc_input")]
pub struct NumbersIncInput {
    pub id: Option<Bigint>,
    #[graphql(name = "link_id")]
    pub link_id: Option<Bigint>,
    pub value: Option<Bigint>,
}
