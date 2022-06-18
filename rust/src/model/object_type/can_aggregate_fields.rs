use crate::model::CanAvgFields;
use crate::model::CanMaxFields;
use crate::model::CanMinFields;
use crate::model::CanSelectColumn;
use crate::model::CanStddevFields;
use crate::model::CanStddevPopFields;
use crate::model::CanStddevSampFields;
use crate::model::CanSumFields;
use crate::model::CanVarPopFields;
use crate::model::CanVarSampFields;
use crate::model::CanVarianceFields;
use async_graphql::*;
#[derive(Debug)]
pub struct CanAggregateFields;
#[Object(name = "can_aggregate_fields")]
impl CanAggregateFields {
    pub async fn avg(&self, ctx: &Context<'_>) -> Option<CanAvgFields> {
        todo!()
    }
    pub async fn count(
        &self,
        ctx: &Context<'_>,
        columns: Option<Vec<CanSelectColumn>>,
        distinct: Option<bool>,
    ) -> i32 {
        todo!()
    }
    pub async fn max(&self, ctx: &Context<'_>) -> Option<CanMaxFields> {
        todo!()
    }
    pub async fn min(&self, ctx: &Context<'_>) -> Option<CanMinFields> {
        todo!()
    }
    pub async fn stddev(&self, ctx: &Context<'_>) -> Option<CanStddevFields> {
        todo!()
    }
    #[graphql(name = "stddev_pop")]
    pub async fn stddev_pop(&self, ctx: &Context<'_>) -> Option<CanStddevPopFields> {
        todo!()
    }
    #[graphql(name = "stddev_samp")]
    pub async fn stddev_samp(&self, ctx: &Context<'_>) -> Option<CanStddevSampFields> {
        todo!()
    }
    pub async fn sum(&self, ctx: &Context<'_>) -> Option<CanSumFields> {
        todo!()
    }
    #[graphql(name = "var_pop")]
    pub async fn var_pop(&self, ctx: &Context<'_>) -> Option<CanVarPopFields> {
        todo!()
    }
    #[graphql(name = "var_samp")]
    pub async fn var_samp(&self, ctx: &Context<'_>) -> Option<CanVarSampFields> {
        todo!()
    }
    pub async fn variance(&self, ctx: &Context<'_>) -> Option<CanVarianceFields> {
        todo!()
    }
}
