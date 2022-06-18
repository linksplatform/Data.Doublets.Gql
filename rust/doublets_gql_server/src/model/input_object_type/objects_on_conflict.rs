use crate::model::ObjectsBoolExp;
use crate::model::ObjectsConstraint;
use crate::model::ObjectsUpdateColumn;
use async_graphql::*;
#[derive(InputObject, Debug)]
#[graphql(name = "objects_on_conflict")]
pub struct ObjectsOnConflict {
    pub constraint: ObjectsConstraint,
    #[graphql(name = "update_columns")]
    pub update_columns: Vec<ObjectsUpdateColumn>,
    pub _where: Option<ObjectsBoolExp>,
}
