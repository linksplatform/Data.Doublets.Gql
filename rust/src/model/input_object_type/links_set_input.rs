use crate::model::Bigint;
use async_graphql::*;
#[derive(InputObject, Debug)]
#[graphql(name = "links_set_input")]
pub struct LinksSetInput {
    #[graphql(name = "from_id")]
    pub from_id: Option<Bigint>,
    pub id: Option<Bigint>,
    #[graphql(name = "to_id")]
    pub to_id: Option<Bigint>,
    #[graphql(name = "type_id")]
    pub type_id: Option<Bigint>,
}
