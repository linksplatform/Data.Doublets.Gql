use crate::model::LinksBoolExp;
use crate::model::LinksConstraint;
use crate::model::LinksUpdateColumn;
use async_graphql::*;

#[derive(InputObject, Debug)]
#[graphql(name = "links_on_conflict")]
pub struct LinksOnConflict {
    pub constraint: LinksConstraint,
    #[graphql(name = "update_columns")]
    pub update_columns: Vec<LinksUpdateColumn>,
    pub _where: Option<Box<LinksBoolExp>>,
}
