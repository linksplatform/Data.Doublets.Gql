use crate::model::MpAvgFields;
use crate::model::MpMaxFields;
use crate::model::MpMinFields;
use crate::model::MpSelectColumn;
use crate::model::MpStddevFields;
use crate::model::MpStddevPopFields;
use crate::model::MpStddevSampFields;
use crate::model::MpSumFields;
use crate::model::MpVarPopFields;
use crate::model::MpVarSampFields;
use crate::model::MpVarianceFields;
use async_graphql::*;

#[derive(Debug)]
pub struct MpAggregateFields;

#[Object(name = "mp_aggregate_fields")]
impl MpAggregateFields {
    pub async fn avg(&self, ctx: &Context<'_>) -> Option<MpAvgFields> {
        todo!()
    }
    pub async fn count(
        &self,
        ctx: &Context<'_>,
        columns: Option<Vec<MpSelectColumn>>,
        distinct: Option<bool>,
    ) -> i32 {
        todo!()
    }
    pub async fn max(&self, ctx: &Context<'_>) -> Option<MpMaxFields> {
        todo!()
    }
    pub async fn min(&self, ctx: &Context<'_>) -> Option<MpMinFields> {
        todo!()
    }
    pub async fn stddev(&self, ctx: &Context<'_>) -> Option<MpStddevFields> {
        todo!()
    }
    #[graphql(name = "stddev_pop")]
    pub async fn stddev_pop(&self, ctx: &Context<'_>) -> Option<MpStddevPopFields> {
        todo!()
    }
    #[graphql(name = "stddev_samp")]
    pub async fn stddev_samp(&self, ctx: &Context<'_>) -> Option<MpStddevSampFields> {
        todo!()
    }
    pub async fn sum(&self, ctx: &Context<'_>) -> Option<MpSumFields> {
        todo!()
    }
    #[graphql(name = "var_pop")]
    pub async fn var_pop(&self, ctx: &Context<'_>) -> Option<MpVarPopFields> {
        todo!()
    }
    #[graphql(name = "var_samp")]
    pub async fn var_samp(&self, ctx: &Context<'_>) -> Option<MpVarSampFields> {
        todo!()
    }
    pub async fn variance(&self, ctx: &Context<'_>) -> Option<MpVarianceFields> {
        todo!()
    }
}
