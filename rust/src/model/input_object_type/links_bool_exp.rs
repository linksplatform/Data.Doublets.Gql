use crate::model::BigintComparisonExp;
use crate::model::CanBoolExp;
use crate::model::JsonbComparisonExp;
use crate::model::MpBoolExp;
use crate::model::NumbersBoolExp;
use crate::model::ObjectsBoolExp;
use crate::model::SelectorsBoolExp;
use crate::model::StringsBoolExp;
use async_graphql::*;
#[derive(InputObject, Debug)]
#[graphql(name = "links_bool_exp")]
pub struct LinksBoolExp {
    #[graphql(name = "_and")]
    pub and: Option<Vec<LinksBoolExp>>,
    #[graphql(name = "_by_group")]
    pub by_group: Option<Box<MpBoolExp>>,
    #[graphql(name = "_by_item")]
    pub by_item: Option<Box<MpBoolExp>>,
    #[graphql(name = "_by_path_item")]
    pub by_path_item: Option<Box<MpBoolExp>>,
    #[graphql(name = "_by_root")]
    pub by_root: Option<Box<MpBoolExp>>,
    #[graphql(name = "_not")]
    pub not: Option<Box<LinksBoolExp>>,
    #[graphql(name = "_or")]
    pub or: Option<Vec<LinksBoolExp>>,
    #[graphql(name = "can_action")]
    pub can_action: Option<CanBoolExp>,
    #[graphql(name = "can_object")]
    pub can_object: Option<CanBoolExp>,
    #[graphql(name = "can_rule")]
    pub can_rule: Option<CanBoolExp>,
    #[graphql(name = "can_subject")]
    pub can_subject: Option<CanBoolExp>,
    pub from: Option<Box<LinksBoolExp>>,
    #[graphql(name = "from_id")]
    pub from_id: Option<BigintComparisonExp>,
    pub id: Option<BigintComparisonExp>,
    pub _in: Option<Box<LinksBoolExp>>,
    pub number: Option<Box<NumbersBoolExp>>,
    pub object: Option<Box<ObjectsBoolExp>>,
    pub out: Option<Box<LinksBoolExp>>,
    pub selected: Option<Box<SelectorsBoolExp>>,
    pub selectors: Option<Box<SelectorsBoolExp>>,
    pub string: Option<Box<StringsBoolExp>>,
    pub to: Option<Box<LinksBoolExp>>,
    #[graphql(name = "to_id")]
    pub to_id: Option<BigintComparisonExp>,
    pub _type: Option<Box<LinksBoolExp>>,
    #[graphql(name = "type_id")]
    pub type_id: Option<BigintComparisonExp>,
    pub typed: Option<Box<LinksBoolExp>>,
    pub value: Option<JsonbComparisonExp>,
}
