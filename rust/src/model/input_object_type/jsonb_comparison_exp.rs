use crate::model::Jsonb;
use async_graphql::*;

#[derive(InputObject, Debug)]
#[graphql(name = "jsonb_comparison_exp")]
pub struct JsonbComparisonExp {
    #[graphql(name = "_contained_in")]
    pub contained_in: Option<Jsonb>,
    #[graphql(name = "_contains")]
    pub contains: Option<Jsonb>,
    #[graphql(name = "_eq")]
    pub eq: Option<Jsonb>,
    #[graphql(name = "_gt")]
    pub gt: Option<Jsonb>,
    #[graphql(name = "_gte")]
    pub gte: Option<Jsonb>,
    #[graphql(name = "_has_key")]
    pub has_key: Option<String>,
    #[graphql(name = "_has_keys_all")]
    pub has_keys_all: Option<Vec<String>>,
    #[graphql(name = "_has_keys_any")]
    pub has_keys_any: Option<Vec<String>>,
    #[graphql(name = "_in")]
    pub _in: Option<Vec<Jsonb>>,
    #[graphql(name = "_is_null")]
    pub is_null: Option<bool>,
    #[graphql(name = "_lt")]
    pub lt: Option<Jsonb>,
    #[graphql(name = "_lte")]
    pub lte: Option<Jsonb>,
    #[graphql(name = "_neq")]
    pub neq: Option<Jsonb>,
    #[graphql(name = "_nin")]
    pub nin: Option<Vec<Jsonb>>,
}
