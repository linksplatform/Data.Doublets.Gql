use async_graphql::*;

#[derive(Enum, Debug, Copy, Clone, Eq, PartialEq)]
#[graphql(name = "links_constraint")]
pub enum LinksConstraint {
    #[graphql(name = "links_pkey")]
    LinksPkey,
}
