use crate::model::Jsonb;
use async_graphql::*;
#[derive(InputObject, Debug)]
#[graphql(name = "objects_prepend_input")]
pub struct ObjectsPrependInput {
    pub value: Option<Jsonb>,
}
