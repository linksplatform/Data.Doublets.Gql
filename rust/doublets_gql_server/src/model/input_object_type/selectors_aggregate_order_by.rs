use crate::model::OrderBy;
use crate::model::SelectorsAvgOrderBy;
use crate::model::SelectorsMaxOrderBy;
use crate::model::SelectorsMinOrderBy;
use crate::model::SelectorsStddevOrderBy;
use crate::model::SelectorsStddevPopOrderBy;
use crate::model::SelectorsStddevSampOrderBy;
use crate::model::SelectorsSumOrderBy;
use crate::model::SelectorsVarPopOrderBy;
use crate::model::SelectorsVarSampOrderBy;
use crate::model::SelectorsVarianceOrderBy;
use async_graphql::*;
#[derive(InputObject, Debug)]
#[graphql(name = "selectors_aggregate_order_by")]
pub struct SelectorsAggregateOrderBy {
    pub avg: Option<SelectorsAvgOrderBy>,
    pub count: Option<OrderBy>,
    pub max: Option<SelectorsMaxOrderBy>,
    pub min: Option<SelectorsMinOrderBy>,
    pub stddev: Option<SelectorsStddevOrderBy>,
    #[graphql(name = "stddev_pop")]
    pub stddev_pop: Option<SelectorsStddevPopOrderBy>,
    #[graphql(name = "stddev_samp")]
    pub stddev_samp: Option<SelectorsStddevSampOrderBy>,
    pub sum: Option<SelectorsSumOrderBy>,
    #[graphql(name = "var_pop")]
    pub var_pop: Option<SelectorsVarPopOrderBy>,
    #[graphql(name = "var_samp")]
    pub var_samp: Option<SelectorsVarSampOrderBy>,
    pub variance: Option<SelectorsVarianceOrderBy>,
}
