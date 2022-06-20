use crate::model::Bigint;
use async_graphql::*;

#[derive(InputObject, Debug)]
#[graphql(name = "links_pk_columns_input")]
pub struct LinksPkColumnsInput {
    pub id: Bigint,
}
