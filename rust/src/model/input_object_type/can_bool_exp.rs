use crate::model::BigintComparisonExp;
use crate::model::LinksBoolExp;
use async_graphql::*;

#[derive(InputObject, Debug)]
#[graphql(name = "can_bool_exp")]
pub struct CanBoolExp {
    #[graphql(name = "_and")]
    pub and: Option<Vec<CanBoolExp>>,
    #[graphql(name = "_not")]
    pub not: Option<Box<CanBoolExp>>,
    #[graphql(name = "_or")]
    pub or: Option<Vec<CanBoolExp>>,
    pub action: Option<Box<LinksBoolExp>>,
    #[graphql(name = "action_id")]
    pub action_id: Option<BigintComparisonExp>,
    pub object: Option<Box<LinksBoolExp>>,
    #[graphql(name = "object_id")]
    pub object_id: Option<BigintComparisonExp>,
    pub rule: Option<Box<LinksBoolExp>>,
    #[graphql(name = "rule_id")]
    pub rule_id: Option<BigintComparisonExp>,
    pub subject: Option<Box<LinksBoolExp>>,
    #[graphql(name = "subject_id")]
    pub subject_id: Option<BigintComparisonExp>,
}
