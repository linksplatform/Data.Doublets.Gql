use crate::model::Bigint;
use async_graphql::*;

#[derive(InputObject, Debug)]
#[graphql(name = "objects_inc_input")]
pub struct ObjectsIncInput {
    pub id: Option<Bigint>,
    #[graphql(name = "link_id")]
    pub link_id: Option<Bigint>,
}
