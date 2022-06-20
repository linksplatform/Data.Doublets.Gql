use crate::model::Bigint;
use async_graphql::*;

#[derive(InputObject, Debug)]
#[graphql(name = "down_links_args")]
pub struct DownLinksArgs {
    pub tree: Option<Bigint>,
}
