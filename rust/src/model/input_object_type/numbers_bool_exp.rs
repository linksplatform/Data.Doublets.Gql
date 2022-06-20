use crate::model::BigintComparisonExp;
use crate::model::LinksBoolExp;
use async_graphql::*;

#[derive(InputObject, Debug)]
#[graphql(name = "numbers_bool_exp")]
pub struct NumbersBoolExp {
    #[graphql(name = "_and")]
    pub and: Option<Vec<NumbersBoolExp>>,
    #[graphql(name = "_not")]
    pub not: Option<Box<NumbersBoolExp>>,
    #[graphql(name = "_or")]
    pub or: Option<Vec<NumbersBoolExp>>,
    pub id: Option<BigintComparisonExp>,
    pub link: Option<LinksBoolExp>,
    #[graphql(name = "link_id")]
    pub link_id: Option<BigintComparisonExp>,
    pub value: Option<BigintComparisonExp>,
}
