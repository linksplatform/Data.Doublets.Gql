use crate::model::NumbersInsertInput;
use crate::model::NumbersOnConflict;
use async_graphql::*;

#[derive(InputObject, Debug)]
#[graphql(name = "numbers_obj_rel_insert_input")]
pub struct NumbersObjRelInsertInput {
    pub data: NumbersInsertInput,
    #[graphql(name = "on_conflict")]
    pub on_conflict: Option<NumbersOnConflict>,
}
