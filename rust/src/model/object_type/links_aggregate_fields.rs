use crate::model::LinksAvgFields;
use crate::model::LinksMaxFields;
use crate::model::LinksMinFields;
use crate::model::LinksSelectColumn;
use crate::model::LinksStddevFields;
use crate::model::LinksStddevPopFields;
use crate::model::LinksStddevSampFields;
use crate::model::LinksSumFields;
use crate::model::LinksVarPopFields;
use crate::model::LinksVarSampFields;
use crate::model::LinksVarianceFields;
use async_graphql::*;

#[derive(Debug)]
pub struct LinksAggregateFields;

#[Object(name = "links_aggregate_fields")]
impl LinksAggregateFields {
    pub async fn avg(&self, ctx: &Context<'_>) -> Option<LinksAvgFields> {
        todo!()
    }
    pub async fn count(
        &self,
        ctx: &Context<'_>,
        columns: Option<Vec<LinksSelectColumn>>,
        distinct: Option<bool>,
    ) -> i32 {
        todo!()
    }
    pub async fn max(&self, ctx: &Context<'_>) -> Option<LinksMaxFields> {
        todo!()
    }
    pub async fn min(&self, ctx: &Context<'_>) -> Option<LinksMinFields> {
        todo!()
    }
    pub async fn stddev(&self, ctx: &Context<'_>) -> Option<LinksStddevFields> {
        todo!()
    }
    #[graphql(name = "stddev_pop")]
    pub async fn stddev_pop(&self, ctx: &Context<'_>) -> Option<LinksStddevPopFields> {
        todo!()
    }
    #[graphql(name = "stddev_samp")]
    pub async fn stddev_samp(&self, ctx: &Context<'_>) -> Option<LinksStddevSampFields> {
        todo!()
    }
    pub async fn sum(&self, ctx: &Context<'_>) -> Option<LinksSumFields> {
        todo!()
    }
    #[graphql(name = "var_pop")]
    pub async fn var_pop(&self, ctx: &Context<'_>) -> Option<LinksVarPopFields> {
        todo!()
    }
    #[graphql(name = "var_samp")]
    pub async fn var_samp(&self, ctx: &Context<'_>) -> Option<LinksVarSampFields> {
        todo!()
    }
    pub async fn variance(&self, ctx: &Context<'_>) -> Option<LinksVarianceFields> {
        todo!()
    }
}
