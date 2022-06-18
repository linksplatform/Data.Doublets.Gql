use crate::model::BigintComparisonExp;
use crate::model::LinksBoolExp;
use async_graphql::*;
#[derive(InputObject, Debug)]
#[graphql(name = "selectors_bool_exp")]
pub struct SelectorsBoolExp {
    #[graphql(name = "_and")]
    pub and: Option<Vec<SelectorsBoolExp>>,
    #[graphql(name = "_not")]
    pub not: Option<Box<SelectorsBoolExp>>,
    #[graphql(name = "_or")]
    pub or: Option<Vec<SelectorsBoolExp>>,
    #[graphql(name = "bool_exp")]
    pub bool_exp: Option<LinksBoolExp>,
    #[graphql(name = "bool_exp_id")]
    pub bool_exp_id: Option<BigintComparisonExp>,
    pub item: Option<LinksBoolExp>,
    #[graphql(name = "item_id")]
    pub item_id: Option<BigintComparisonExp>,
    pub selector: Option<LinksBoolExp>,
    #[graphql(name = "selector_id")]
    pub selector_id: Option<BigintComparisonExp>,
    #[graphql(name = "selector_include_id")]
    pub selector_include_id: Option<BigintComparisonExp>,
}
