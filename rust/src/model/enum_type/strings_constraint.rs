use async_graphql::*;

#[derive(Enum, Debug, Copy, Clone, Eq, PartialEq)]
#[graphql(name = "strings_constraint")]
pub enum StringsConstraint {
    #[graphql(name = "strings_pkey")]
    StringsPkey,
}
