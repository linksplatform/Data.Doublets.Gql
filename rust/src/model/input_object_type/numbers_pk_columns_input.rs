use crate::model::Bigint;
use async_graphql::*;

#[derive(InputObject, Debug)]
#[graphql(name = "numbers_pk_columns_input")]
pub struct NumbersPkColumnsInput {
    pub id: Bigint,
}
