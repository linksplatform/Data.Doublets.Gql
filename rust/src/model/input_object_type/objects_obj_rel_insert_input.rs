use crate::model::ObjectsInsertInput;
use crate::model::ObjectsOnConflict;
use async_graphql::*;

#[derive(InputObject, Debug)]
#[graphql(name = "objects_obj_rel_insert_input")]
pub struct ObjectsObjRelInsertInput {
    pub data: ObjectsInsertInput,
    #[graphql(name = "on_conflict")]
    pub on_conflict: Option<ObjectsOnConflict>,
}
