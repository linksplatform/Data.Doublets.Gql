use crate::model::Bigint;
use async_graphql::*;

#[derive(InputObject, Debug)]
#[graphql(name = "mp_pk_columns_input")]
pub struct MpPkColumnsInput {
    pub id: Bigint,
}
