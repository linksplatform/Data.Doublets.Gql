use crate::model::Bigint;
use async_graphql::*;

#[derive(InputObject, Debug)]
#[graphql(name = "up_links_args")]
pub struct UpLinksArgs {
    pub tree: Option<Bigint>,
}
