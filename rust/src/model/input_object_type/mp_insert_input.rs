use crate::model::Bigint;
use crate::model::LinksObjRelInsertInput;
use crate::model::MpArrRelInsertInput;
use async_graphql::*;

#[derive(InputObject, Debug)]
#[graphql(name = "mp_insert_input")]
pub struct MpInsertInput {
    #[graphql(name = "by_group")]
    pub by_group: Option<LinksObjRelInsertInput>,
    #[graphql(name = "by_item")]
    pub by_item: Option<MpArrRelInsertInput>,
    #[graphql(name = "by_path_item")]
    pub by_path_item: Option<MpArrRelInsertInput>,
    #[graphql(name = "by_position")]
    pub by_position: Option<MpArrRelInsertInput>,
    #[graphql(name = "by_root")]
    pub by_root: Option<MpArrRelInsertInput>,
    #[graphql(name = "group_id")]
    pub group_id: Option<Bigint>,
    pub id: Option<Bigint>,
    #[graphql(name = "insert_category")]
    pub insert_category: Option<String>,
    pub item: Option<LinksObjRelInsertInput>,
    #[graphql(name = "item_id")]
    pub item_id: Option<Bigint>,
    #[graphql(name = "path_item")]
    pub path_item: Option<LinksObjRelInsertInput>,
    #[graphql(name = "path_item_depth")]
    pub path_item_depth: Option<Bigint>,
    #[graphql(name = "path_item_id")]
    pub path_item_id: Option<Bigint>,
    #[graphql(name = "position_id")]
    pub position_id: Option<String>,
    pub root: Option<LinksObjRelInsertInput>,
    #[graphql(name = "root_id")]
    pub root_id: Option<Bigint>,
}
