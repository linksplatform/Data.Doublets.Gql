use async_graphql::*;
#[derive(InputObject, Debug)]
#[graphql(name = "objects_delete_at_path_input")]
pub struct ObjectsDeleteAtPathInput {
    pub value: Option<Vec<String>>,
}
