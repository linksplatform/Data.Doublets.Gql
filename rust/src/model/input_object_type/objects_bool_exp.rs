use crate::model::BigintComparisonExp;
use crate::model::JsonbComparisonExp;
use crate::model::LinksBoolExp;
use async_graphql::*;

#[derive(InputObject, Debug)]
#[graphql(name = "objects_bool_exp")]
pub struct ObjectsBoolExp {
    #[graphql(name = "_and")]
    pub and: Option<Vec<ObjectsBoolExp>>,
    #[graphql(name = "_not")]
    pub not: Option<Box<ObjectsBoolExp>>,
    #[graphql(name = "_or")]
    pub or: Option<Vec<ObjectsBoolExp>>,
    pub id: Option<BigintComparisonExp>,
    pub link: Option<Box<LinksBoolExp>>,
    #[graphql(name = "link_id")]
    pub link_id: Option<BigintComparisonExp>,
    pub value: Option<JsonbComparisonExp>,
}
