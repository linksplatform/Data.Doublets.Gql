use crate::model::Bigint;
use crate::model::LinksObjRelInsertInput;
use async_graphql::*;

#[derive(InputObject, Debug)]
#[graphql(name = "strings_insert_input")]
pub struct StringsInsertInput {
    pub id: Option<Bigint>,
    pub link: Option<LinksObjRelInsertInput>,
    #[graphql(name = "link_id")]
    pub link_id: Option<Bigint>,
    pub value: Option<String>,
}
