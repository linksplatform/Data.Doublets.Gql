use crate::model::NumbersAvgFields;
use crate::model::NumbersMaxFields;
use crate::model::NumbersMinFields;
use crate::model::NumbersSelectColumn;
use crate::model::NumbersStddevFields;
use crate::model::NumbersStddevPopFields;
use crate::model::NumbersStddevSampFields;
use crate::model::NumbersSumFields;
use crate::model::NumbersVarPopFields;
use crate::model::NumbersVarSampFields;
use crate::model::NumbersVarianceFields;
use async_graphql::*;
#[derive(Debug)]
pub struct NumbersAggregateFields;
#[Object(name = "numbers_aggregate_fields")]
impl NumbersAggregateFields {
    pub async fn avg(&self, ctx: &Context<'_>) -> Option<NumbersAvgFields> {
        todo!()
    }
    pub async fn count(
        &self,
        ctx: &Context<'_>,
        columns: Option<Vec<NumbersSelectColumn>>,
        distinct: Option<bool>,
    ) -> i32 {
        todo!()
    }
    pub async fn max(&self, ctx: &Context<'_>) -> Option<NumbersMaxFields> {
        todo!()
    }
    pub async fn min(&self, ctx: &Context<'_>) -> Option<NumbersMinFields> {
        todo!()
    }
    pub async fn stddev(&self, ctx: &Context<'_>) -> Option<NumbersStddevFields> {
        todo!()
    }
    #[graphql(name = "stddev_pop")]
    pub async fn stddev_pop(&self, ctx: &Context<'_>) -> Option<NumbersStddevPopFields> {
        todo!()
    }
    #[graphql(name = "stddev_samp")]
    pub async fn stddev_samp(&self, ctx: &Context<'_>) -> Option<NumbersStddevSampFields> {
        todo!()
    }
    pub async fn sum(&self, ctx: &Context<'_>) -> Option<NumbersSumFields> {
        todo!()
    }
    #[graphql(name = "var_pop")]
    pub async fn var_pop(&self, ctx: &Context<'_>) -> Option<NumbersVarPopFields> {
        todo!()
    }
    #[graphql(name = "var_samp")]
    pub async fn var_samp(&self, ctx: &Context<'_>) -> Option<NumbersVarSampFields> {
        todo!()
    }
    pub async fn variance(&self, ctx: &Context<'_>) -> Option<NumbersVarianceFields> {
        todo!()
    }
}
