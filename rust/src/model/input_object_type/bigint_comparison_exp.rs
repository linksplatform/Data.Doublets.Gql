use crate::model::Bigint;
use async_graphql::*;
#[derive(InputObject, Debug)]
#[graphql(name = "bigint_comparison_exp")]
pub struct BigintComparisonExp {
    #[graphql(name = "_eq")]
    pub eq: Option<Bigint>,
    #[graphql(name = "_gt")]
    pub gt: Option<Bigint>,
    #[graphql(name = "_gte")]
    pub gte: Option<Bigint>,
    #[graphql(name = "_in")]
    pub _in: Option<Vec<Bigint>>,
    #[graphql(name = "_is_null")]
    pub is_null: Option<bool>,
    #[graphql(name = "_lt")]
    pub lt: Option<Bigint>,
    #[graphql(name = "_lte")]
    pub lte: Option<Bigint>,
    #[graphql(name = "_neq")]
    pub neq: Option<Bigint>,
    #[graphql(name = "_nin")]
    pub nin: Option<Vec<Bigint>>,
}
