use crate::model::Bigint;
use async_graphql::*;
#[derive(InputObject, Debug)]
#[graphql(name = "objects_pk_columns_input")]
pub struct ObjectsPkColumnsInput {
    pub id: Bigint,
}
