use async_graphql::*;
#[derive(Enum, Debug, Copy, Clone, Eq, PartialEq)]
#[graphql(name = "objects_constraint")]
pub enum ObjectsConstraint {
    #[graphql(name = "objects_pkey")]
    ObjectsPkey,
}
