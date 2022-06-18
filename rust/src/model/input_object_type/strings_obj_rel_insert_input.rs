use crate::model::StringsInsertInput;
use crate::model::StringsOnConflict;
use async_graphql::*;
#[derive(InputObject, Debug)]
#[graphql(name = "strings_obj_rel_insert_input")]
pub struct StringsObjRelInsertInput {
    pub data: StringsInsertInput,
    #[graphql(name = "on_conflict")]
    pub on_conflict: Option<StringsOnConflict>,
}
