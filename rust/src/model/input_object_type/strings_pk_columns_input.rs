use crate::model::Bigint;
use async_graphql::*;

#[derive(InputObject, Debug)]
#[graphql(name = "strings_pk_columns_input")]
pub struct StringsPkColumnsInput {
    pub id: Bigint,
}
