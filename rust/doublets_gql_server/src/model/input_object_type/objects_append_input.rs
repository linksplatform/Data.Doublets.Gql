use crate::model::Jsonb;
use async_graphql::*;
#[derive(InputObject, Debug)]
#[graphql(name = "objects_append_input")]
pub struct ObjectsAppendInput {
    pub value: Option<Jsonb>,
}
