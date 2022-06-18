use crate::model::Strings;
use crate::model::StringsAggregateFields;
use async_graphql::*;
#[derive(Debug)]
pub struct StringsAggregate;
#[Object(name = "strings_aggregate")]
impl StringsAggregate {
    pub async fn aggregate(&self, ctx: &Context<'_>) -> Option<StringsAggregateFields> {
        todo!()
    }
    pub async fn nodes(&self, ctx: &Context<'_>) -> Vec<Strings> {
        todo!()
    }
}
