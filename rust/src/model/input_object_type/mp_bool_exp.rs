use crate::model::BigintComparisonExp;
use crate::model::LinksBoolExp;
use crate::model::StringComparisonExp;
use async_graphql::*;

#[derive(InputObject, Debug)]
#[graphql(name = "mp_bool_exp")]
pub struct MpBoolExp {
    #[graphql(name = "_and")]
    pub and: Option<Vec<MpBoolExp>>,
    #[graphql(name = "_not")]
    pub not: Option<Box<MpBoolExp>>,
    #[graphql(name = "_or")]
    pub or: Option<Vec<MpBoolExp>>,
    #[graphql(name = "by_group")]
    pub by_group: Option<Box<LinksBoolExp>>,
    #[graphql(name = "by_item")]
    pub by_item: Option<Box<MpBoolExp>>,
    #[graphql(name = "by_path_item")]
    pub by_path_item: Option<Box<MpBoolExp>>,
    #[graphql(name = "by_position")]
    pub by_position: Option<Box<MpBoolExp>>,
    #[graphql(name = "by_root")]
    pub by_root: Option<Box<MpBoolExp>>,
    #[graphql(name = "group_id")]
    pub group_id: Option<BigintComparisonExp>,
    pub id: Option<BigintComparisonExp>,
    #[graphql(name = "insert_category")]
    pub insert_category: Option<StringComparisonExp>,
    pub item: Option<Box<LinksBoolExp>>,
    #[graphql(name = "item_id")]
    pub item_id: Option<BigintComparisonExp>,
    #[graphql(name = "path_item")]
    pub path_item: Option<Box<LinksBoolExp>>,
    #[graphql(name = "path_item_depth")]
    pub path_item_depth: Option<BigintComparisonExp>,
    #[graphql(name = "path_item_id")]
    pub path_item_id: Option<BigintComparisonExp>,
    #[graphql(name = "position_id")]
    pub position_id: Option<StringComparisonExp>,
    pub root: Option<Box<LinksBoolExp>>,
    #[graphql(name = "root_id")]
    pub root_id: Option<BigintComparisonExp>,
}
