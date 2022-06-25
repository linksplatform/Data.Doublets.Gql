use crate::model::StringsBoolExp;
use crate::model::StringsConstraint;
use crate::model::StringsUpdateColumn;
use async_graphql::*;

#[derive(InputObject, Debug)]
#[graphql(name = "strings_on_conflict")]
pub struct StringsOnConflict {
    pub constraint: StringsConstraint,
    #[graphql(name = "update_columns")]
    pub update_columns: Vec<StringsUpdateColumn>,
    pub _where: Option<StringsBoolExp>,
}
