use crate::model::SelectorsAvgFields;
use crate::model::SelectorsMaxFields;
use crate::model::SelectorsMinFields;
use crate::model::SelectorsSelectColumn;
use crate::model::SelectorsStddevFields;
use crate::model::SelectorsStddevPopFields;
use crate::model::SelectorsStddevSampFields;
use crate::model::SelectorsSumFields;
use crate::model::SelectorsVarPopFields;
use crate::model::SelectorsVarSampFields;
use crate::model::SelectorsVarianceFields;
use async_graphql::*;

#[derive(Debug)]
pub struct SelectorsAggregateFields;

#[Object(name = "selectors_aggregate_fields")]
impl SelectorsAggregateFields {
    pub async fn avg(&self, ctx: &Context<'_>) -> Option<SelectorsAvgFields> {
        todo!()
    }
    pub async fn count(
        &self,
        ctx: &Context<'_>,
        columns: Option<Vec<SelectorsSelectColumn>>,
        distinct: Option<bool>,
    ) -> i32 {
        todo!()
    }
    pub async fn max(&self, ctx: &Context<'_>) -> Option<SelectorsMaxFields> {
        todo!()
    }
    pub async fn min(&self, ctx: &Context<'_>) -> Option<SelectorsMinFields> {
        todo!()
    }
    pub async fn stddev(&self, ctx: &Context<'_>) -> Option<SelectorsStddevFields> {
        todo!()
    }
    #[graphql(name = "stddev_pop")]
    pub async fn stddev_pop(&self, ctx: &Context<'_>) -> Option<SelectorsStddevPopFields> {
        todo!()
    }
    #[graphql(name = "stddev_samp")]
    pub async fn stddev_samp(&self, ctx: &Context<'_>) -> Option<SelectorsStddevSampFields> {
        todo!()
    }
    pub async fn sum(&self, ctx: &Context<'_>) -> Option<SelectorsSumFields> {
        todo!()
    }
    #[graphql(name = "var_pop")]
    pub async fn var_pop(&self, ctx: &Context<'_>) -> Option<SelectorsVarPopFields> {
        todo!()
    }
    #[graphql(name = "var_samp")]
    pub async fn var_samp(&self, ctx: &Context<'_>) -> Option<SelectorsVarSampFields> {
        todo!()
    }
    pub async fn variance(&self, ctx: &Context<'_>) -> Option<SelectorsVarianceFields> {
        todo!()
    }
}
