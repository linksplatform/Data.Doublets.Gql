use crate::model::ObjectsAvgFields;
use crate::model::ObjectsMaxFields;
use crate::model::ObjectsMinFields;
use crate::model::ObjectsSelectColumn;
use crate::model::ObjectsStddevFields;
use crate::model::ObjectsStddevPopFields;
use crate::model::ObjectsStddevSampFields;
use crate::model::ObjectsSumFields;
use crate::model::ObjectsVarPopFields;
use crate::model::ObjectsVarSampFields;
use crate::model::ObjectsVarianceFields;
use async_graphql::*;

#[derive(Debug)]
pub struct ObjectsAggregateFields;

#[Object(name = "objects_aggregate_fields")]
impl ObjectsAggregateFields {
    pub async fn avg(&self, ctx: &Context<'_>) -> Option<ObjectsAvgFields> {
        todo!()
    }
    pub async fn count(
        &self,
        ctx: &Context<'_>,
        columns: Option<Vec<ObjectsSelectColumn>>,
        distinct: Option<bool>,
    ) -> i32 {
        todo!()
    }
    pub async fn max(&self, ctx: &Context<'_>) -> Option<ObjectsMaxFields> {
        todo!()
    }
    pub async fn min(&self, ctx: &Context<'_>) -> Option<ObjectsMinFields> {
        todo!()
    }
    pub async fn stddev(&self, ctx: &Context<'_>) -> Option<ObjectsStddevFields> {
        todo!()
    }
    #[graphql(name = "stddev_pop")]
    pub async fn stddev_pop(&self, ctx: &Context<'_>) -> Option<ObjectsStddevPopFields> {
        todo!()
    }
    #[graphql(name = "stddev_samp")]
    pub async fn stddev_samp(&self, ctx: &Context<'_>) -> Option<ObjectsStddevSampFields> {
        todo!()
    }
    pub async fn sum(&self, ctx: &Context<'_>) -> Option<ObjectsSumFields> {
        todo!()
    }
    #[graphql(name = "var_pop")]
    pub async fn var_pop(&self, ctx: &Context<'_>) -> Option<ObjectsVarPopFields> {
        todo!()
    }
    #[graphql(name = "var_samp")]
    pub async fn var_samp(&self, ctx: &Context<'_>) -> Option<ObjectsVarSampFields> {
        todo!()
    }
    pub async fn variance(&self, ctx: &Context<'_>) -> Option<ObjectsVarianceFields> {
        todo!()
    }
}
