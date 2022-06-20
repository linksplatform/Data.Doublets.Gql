use crate::model::MpBoolExp;
use crate::model::MpConstraint;
use crate::model::MpUpdateColumn;
use async_graphql::*;

#[derive(InputObject, Debug)]
#[graphql(name = "mp_on_conflict")]
pub struct MpOnConflict {
    pub constraint: MpConstraint,
    #[graphql(name = "update_columns")]
    pub update_columns: Vec<MpUpdateColumn>,
    pub _where: Option<MpBoolExp>,
}
