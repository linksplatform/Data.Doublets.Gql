use crate::model::Bigint;
use crate::model::LinksArrRelInsertInput;
use crate::model::LinksObjRelInsertInput;
use async_graphql::*;

#[derive(InputObject, Debug)]
#[graphql(name = "selectors_insert_input")]
pub struct SelectorsInsertInput {
    #[graphql(name = "bool_exp")]
    pub bool_exp: Option<LinksArrRelInsertInput>,
    #[graphql(name = "bool_exp_id")]
    pub bool_exp_id: Option<Bigint>,
    pub item: Option<LinksObjRelInsertInput>,
    #[graphql(name = "item_id")]
    pub item_id: Option<Bigint>,
    pub selector: Option<LinksObjRelInsertInput>,
    #[graphql(name = "selector_id")]
    pub selector_id: Option<Bigint>,
    #[graphql(name = "selector_include_id")]
    pub selector_include_id: Option<Bigint>,
}
