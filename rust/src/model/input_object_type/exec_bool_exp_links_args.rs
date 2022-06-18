use crate::model::Bigint;
use async_graphql::*;
#[derive(InputObject, Debug)]
#[graphql(name = "exec_bool_exp_links_args")]
pub struct ExecBoolExpLinksArgs {
    #[graphql(name = "link_id")]
    pub link_id: Option<Bigint>,
}
