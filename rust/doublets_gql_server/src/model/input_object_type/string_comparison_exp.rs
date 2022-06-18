use async_graphql::*;
#[derive(InputObject, Debug)]
#[graphql(name = "String_comparison_exp")]
pub struct StringComparisonExp {
    #[graphql(name = "_eq")]
    pub eq: Option<String>,
    #[graphql(name = "_gt")]
    pub gt: Option<String>,
    #[graphql(name = "_gte")]
    pub gte: Option<String>,
    #[graphql(name = "_ilike")]
    pub ilike: Option<String>,
    #[graphql(name = "_in")]
    pub _in: Option<Vec<String>>,
    #[graphql(name = "_iregex")]
    pub iregex: Option<String>,
    #[graphql(name = "_is_null")]
    pub is_null: Option<bool>,
    #[graphql(name = "_like")]
    pub like: Option<String>,
    #[graphql(name = "_lt")]
    pub lt: Option<String>,
    #[graphql(name = "_lte")]
    pub lte: Option<String>,
    #[graphql(name = "_neq")]
    pub neq: Option<String>,
    #[graphql(name = "_nilike")]
    pub nilike: Option<String>,
    #[graphql(name = "_nin")]
    pub nin: Option<Vec<String>>,
    #[graphql(name = "_niregex")]
    pub niregex: Option<String>,
    #[graphql(name = "_nlike")]
    pub nlike: Option<String>,
    #[graphql(name = "_nregex")]
    pub nregex: Option<String>,
    #[graphql(name = "_nsimilar")]
    pub nsimilar: Option<String>,
    #[graphql(name = "_regex")]
    pub regex: Option<String>,
    #[graphql(name = "_similar")]
    pub similar: Option<String>,
}
