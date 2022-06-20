use crate::model::Bigint;
use crate::model::LinksObjRelInsertInput;
use async_graphql::*;

#[derive(InputObject, Debug)]
#[graphql(name = "can_insert_input")]
pub struct CanInsertInput {
    pub action: Option<LinksObjRelInsertInput>,
    #[graphql(name = "action_id")]
    pub action_id: Option<Bigint>,
    pub object: Option<LinksObjRelInsertInput>,
    #[graphql(name = "object_id")]
    pub object_id: Option<Bigint>,
    pub rule: Option<LinksObjRelInsertInput>,
    #[graphql(name = "rule_id")]
    pub rule_id: Option<Bigint>,
    pub subject: Option<LinksObjRelInsertInput>,
    #[graphql(name = "subject_id")]
    pub subject_id: Option<Bigint>,
}
