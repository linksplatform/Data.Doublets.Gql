use async_graphql::*;
#[derive(InputObject, Debug)]
#[graphql(name = "objects_delete_key_input")]
pub struct ObjectsDeleteKeyInput {
    pub value: Option<String>,
}
