use async_graphql::*;

#[derive(InputObject, Debug)]
#[graphql(name = "objects_delete_elem_input")]
pub struct ObjectsDeleteElemInput {
    pub value: Option<i32>,
}
