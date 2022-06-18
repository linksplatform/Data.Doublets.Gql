use crate::model::MpInsertInput;
use crate::model::MpOnConflict;
use async_graphql::*;
#[derive(InputObject, Debug)]
#[graphql(name = "mp_arr_rel_insert_input")]
pub struct MpArrRelInsertInput {
    pub data: Vec<MpInsertInput>,
    #[graphql(name = "on_conflict")]
    pub on_conflict: Option<MpOnConflict>,
}
