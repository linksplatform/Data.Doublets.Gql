use async_graphql::*;

#[derive(Enum, Debug, Copy, Clone, Eq, PartialEq)]
#[graphql(name = "numbers_constraint")]
pub enum NumbersConstraint {
    #[graphql(name = "numbers_pkey")]
    NumbersPkey,
}
