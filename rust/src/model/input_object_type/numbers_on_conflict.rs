use crate::model::NumbersBoolExp;
use crate::model::NumbersConstraint;
use crate::model::NumbersUpdateColumn;
use async_graphql::*;
#[derive(InputObject, Debug)]
#[graphql(name = "numbers_on_conflict")]
pub struct NumbersOnConflict {
    pub constraint: NumbersConstraint,
    #[graphql(name = "update_columns")]
    pub update_columns: Vec<NumbersUpdateColumn>,
    pub _where: Option<NumbersBoolExp>,
}
