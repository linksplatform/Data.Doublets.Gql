use crate::model::LinksInsertInput;
use crate::model::LinksOnConflict;
use async_graphql::*;
#[derive(InputObject, Debug)]
#[graphql(name = "links_arr_rel_insert_input")]
pub struct LinksArrRelInsertInput {
    pub data: Vec<LinksInsertInput>,
    #[graphql(name = "on_conflict")]
    pub on_conflict: Option<LinksOnConflict>,
}
