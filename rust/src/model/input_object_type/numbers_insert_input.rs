use crate::model::Bigint;
use crate::model::LinksObjRelInsertInput;
use async_graphql::*;

#[derive(InputObject, Debug)]
#[graphql(name = "numbers_insert_input")]
pub struct NumbersInsertInput {
    pub id: Option<Bigint>,
    pub link: Option<LinksObjRelInsertInput>,
    #[graphql(name = "link_id")]
    pub link_id: Option<Bigint>,
    pub value: Option<Bigint>,
}
