use async_graphql::*;

#[derive(Enum, Debug, Copy, Clone, Eq, PartialEq)]
#[graphql(name = "can_select_column")]
pub enum CanSelectColumn {
    #[graphql(name = "action_id")]
    ActionId,
    #[graphql(name = "object_id")]
    ObjectId,
    #[graphql(name = "rule_id")]
    RuleId,
    #[graphql(name = "subject_id")]
    SubjectId,
}
