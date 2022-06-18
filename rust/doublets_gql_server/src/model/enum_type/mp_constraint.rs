use async_graphql::*;
#[derive(Enum, Debug, Copy, Clone, Eq, PartialEq)]
#[graphql(name = "mp_constraint")]
pub enum MpConstraint {
    #[graphql(name = "mp_pkey")]
    MpPkey,
}
