use crate::model::MpAvgOrderBy;
use crate::model::MpMaxOrderBy;
use crate::model::MpMinOrderBy;
use crate::model::MpStddevOrderBy;
use crate::model::MpStddevPopOrderBy;
use crate::model::MpStddevSampOrderBy;
use crate::model::MpSumOrderBy;
use crate::model::MpVarPopOrderBy;
use crate::model::MpVarSampOrderBy;
use crate::model::MpVarianceOrderBy;
use crate::model::OrderBy;
use async_graphql::*;
#[derive(InputObject, Debug)]
#[graphql(name = "mp_aggregate_order_by")]
pub struct MpAggregateOrderBy {
    pub avg: Option<MpAvgOrderBy>,
    pub count: Option<OrderBy>,
    pub max: Option<MpMaxOrderBy>,
    pub min: Option<MpMinOrderBy>,
    pub stddev: Option<MpStddevOrderBy>,
    #[graphql(name = "stddev_pop")]
    pub stddev_pop: Option<MpStddevPopOrderBy>,
    #[graphql(name = "stddev_samp")]
    pub stddev_samp: Option<MpStddevSampOrderBy>,
    pub sum: Option<MpSumOrderBy>,
    #[graphql(name = "var_pop")]
    pub var_pop: Option<MpVarPopOrderBy>,
    #[graphql(name = "var_samp")]
    pub var_samp: Option<MpVarSampOrderBy>,
    pub variance: Option<MpVarianceOrderBy>,
}
