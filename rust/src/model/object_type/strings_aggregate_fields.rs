use crate::model::StringsAvgFields;
use crate::model::StringsMaxFields;
use crate::model::StringsMinFields;
use crate::model::StringsSelectColumn;
use crate::model::StringsStddevFields;
use crate::model::StringsStddevPopFields;
use crate::model::StringsStddevSampFields;
use crate::model::StringsSumFields;
use crate::model::StringsVarPopFields;
use crate::model::StringsVarSampFields;
use crate::model::StringsVarianceFields;
use async_graphql::*;
#[derive(Debug)]
pub struct StringsAggregateFields;
#[Object(name = "strings_aggregate_fields")]
impl StringsAggregateFields {
    pub async fn avg(&self, ctx: &Context<'_>) -> Option<StringsAvgFields> {
        todo!()
    }
    pub async fn count(
        &self,
        ctx: &Context<'_>,
        columns: Option<Vec<StringsSelectColumn>>,
        distinct: Option<bool>,
    ) -> i32 {
        todo!()
    }
    pub async fn max(&self, ctx: &Context<'_>) -> Option<StringsMaxFields> {
        todo!()
    }
    pub async fn min(&self, ctx: &Context<'_>) -> Option<StringsMinFields> {
        todo!()
    }
    pub async fn stddev(&self, ctx: &Context<'_>) -> Option<StringsStddevFields> {
        todo!()
    }
    #[graphql(name = "stddev_pop")]
    pub async fn stddev_pop(&self, ctx: &Context<'_>) -> Option<StringsStddevPopFields> {
        todo!()
    }
    #[graphql(name = "stddev_samp")]
    pub async fn stddev_samp(&self, ctx: &Context<'_>) -> Option<StringsStddevSampFields> {
        todo!()
    }
    pub async fn sum(&self, ctx: &Context<'_>) -> Option<StringsSumFields> {
        todo!()
    }
    #[graphql(name = "var_pop")]
    pub async fn var_pop(&self, ctx: &Context<'_>) -> Option<StringsVarPopFields> {
        todo!()
    }
    #[graphql(name = "var_samp")]
    pub async fn var_samp(&self, ctx: &Context<'_>) -> Option<StringsVarSampFields> {
        todo!()
    }
    pub async fn variance(&self, ctx: &Context<'_>) -> Option<StringsVarianceFields> {
        todo!()
    }
}
