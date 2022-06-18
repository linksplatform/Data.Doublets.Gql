use crate::model::CanInsertInput;
use async_graphql::*;
#[derive(InputObject, Debug)]
#[graphql(name = "can_arr_rel_insert_input")]
pub struct CanArrRelInsertInput {
    pub data: Vec<CanInsertInput>,
}
