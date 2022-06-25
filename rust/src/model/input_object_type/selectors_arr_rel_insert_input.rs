use crate::model::SelectorsInsertInput;
use async_graphql::*;

#[derive(InputObject, Debug)]
#[graphql(name = "selectors_arr_rel_insert_input")]
pub struct SelectorsArrRelInsertInput {
    pub data: Vec<SelectorsInsertInput>,
}
