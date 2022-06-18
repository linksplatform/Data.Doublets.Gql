use crate::model::Bigint;
use crate::model::Jsonb;
use async_graphql::*;
#[derive(InputObject, Debug)]
#[graphql(name = "objects_set_input")]
pub struct ObjectsSetInput {
    pub id: Option<Bigint>,
    #[graphql(name = "link_id")]
    pub link_id: Option<Bigint>,
    pub value: Option<Jsonb>,
}
