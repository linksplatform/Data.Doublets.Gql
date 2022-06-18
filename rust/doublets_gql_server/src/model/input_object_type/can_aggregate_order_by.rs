use crate::model::CanAvgOrderBy;
use crate::model::CanMaxOrderBy;
use crate::model::CanMinOrderBy;
use crate::model::CanStddevOrderBy;
use crate::model::CanStddevPopOrderBy;
use crate::model::CanStddevSampOrderBy;
use crate::model::CanSumOrderBy;
use crate::model::CanVarPopOrderBy;
use crate::model::CanVarSampOrderBy;
use crate::model::CanVarianceOrderBy;
use crate::model::OrderBy;
use async_graphql::*;
#[derive(InputObject, Debug)]
#[graphql(name = "can_aggregate_order_by")]
pub struct CanAggregateOrderBy {
    pub avg: Option<CanAvgOrderBy>,
    pub count: Option<OrderBy>,
    pub max: Option<CanMaxOrderBy>,
    pub min: Option<CanMinOrderBy>,
    pub stddev: Option<CanStddevOrderBy>,
    #[graphql(name = "stddev_pop")]
    pub stddev_pop: Option<CanStddevPopOrderBy>,
    #[graphql(name = "stddev_samp")]
    pub stddev_samp: Option<CanStddevSampOrderBy>,
    pub sum: Option<CanSumOrderBy>,
    #[graphql(name = "var_pop")]
    pub var_pop: Option<CanVarPopOrderBy>,
    #[graphql(name = "var_samp")]
    pub var_samp: Option<CanVarSampOrderBy>,
    pub variance: Option<CanVarianceOrderBy>,
}
