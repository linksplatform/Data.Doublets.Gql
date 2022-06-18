use crate::model::Bigint;
use crate::model::CanArrRelInsertInput;
use crate::model::LinksArrRelInsertInput;
use crate::model::LinksObjRelInsertInput;
use crate::model::MpArrRelInsertInput;
use crate::model::NumbersObjRelInsertInput;
use crate::model::ObjectsObjRelInsertInput;
use crate::model::SelectorsArrRelInsertInput;
use crate::model::StringsObjRelInsertInput;
use async_graphql::*;
#[derive(InputObject, Debug)]
#[graphql(name = "links_insert_input")]
pub struct LinksInsertInput {
    #[graphql(name = "_by_group")]
    pub by_group: Option<MpArrRelInsertInput>,
    #[graphql(name = "_by_item")]
    pub by_item: Option<MpArrRelInsertInput>,
    #[graphql(name = "_by_path_item")]
    pub by_path_item: Option<MpArrRelInsertInput>,
    #[graphql(name = "_by_root")]
    pub by_root: Option<MpArrRelInsertInput>,
    #[graphql(name = "can_action")]
    pub can_action: Option<CanArrRelInsertInput>,
    #[graphql(name = "can_object")]
    pub can_object: Option<CanArrRelInsertInput>,
    #[graphql(name = "can_rule")]
    pub can_rule: Option<CanArrRelInsertInput>,
    #[graphql(name = "can_subject")]
    pub can_subject: Option<CanArrRelInsertInput>,
    pub from: Option<Box<LinksObjRelInsertInput>>,
    #[graphql(name = "from_id")]
    pub from_id: Option<Bigint>,
    pub id: Option<Bigint>,
    pub _in: Option<LinksArrRelInsertInput>,
    pub number: Option<Box<NumbersObjRelInsertInput>>,
    pub object: Option<Box<ObjectsObjRelInsertInput>>,
    pub out: Option<LinksArrRelInsertInput>,
    pub selected: Option<SelectorsArrRelInsertInput>,
    pub selectors: Option<SelectorsArrRelInsertInput>,
    pub string: Option<Box<StringsObjRelInsertInput>>,
    pub to: Option<Box<LinksObjRelInsertInput>>,
    #[graphql(name = "to_id")]
    pub to_id: Option<Bigint>,
    pub _type: Option<Box<LinksObjRelInsertInput>>,
    #[graphql(name = "type_id")]
    pub type_id: Option<Bigint>,
    pub typed: Option<LinksArrRelInsertInput>,
}
