use crate::model::Bigint;
use async_graphql::*;

#[derive(InputObject, Debug)]
#[graphql(name = "strings_set_input")]
pub struct StringsSetInput {
    pub id: Option<Bigint>,
    #[graphql(name = "link_id")]
    pub link_id: Option<Bigint>,
    pub value: Option<String>,
}
