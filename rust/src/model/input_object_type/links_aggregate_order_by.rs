use crate::model::LinksAvgOrderBy;
use crate::model::LinksMaxOrderBy;
use crate::model::LinksMinOrderBy;
use crate::model::LinksStddevOrderBy;
use crate::model::LinksStddevPopOrderBy;
use crate::model::LinksStddevSampOrderBy;
use crate::model::LinksSumOrderBy;
use crate::model::LinksVarPopOrderBy;
use crate::model::LinksVarSampOrderBy;
use crate::model::LinksVarianceOrderBy;
use crate::model::OrderBy;
use async_graphql::*;

#[derive(InputObject, Debug)]
#[graphql(name = "links_aggregate_order_by")]
pub struct LinksAggregateOrderBy {
    pub avg: Option<LinksAvgOrderBy>,
    pub count: Option<OrderBy>,
    pub max: Option<LinksMaxOrderBy>,
    pub min: Option<LinksMinOrderBy>,
    pub stddev: Option<LinksStddevOrderBy>,
    #[graphql(name = "stddev_pop")]
    pub stddev_pop: Option<LinksStddevPopOrderBy>,
    #[graphql(name = "stddev_samp")]
    pub stddev_samp: Option<LinksStddevSampOrderBy>,
    pub sum: Option<LinksSumOrderBy>,
    #[graphql(name = "var_pop")]
    pub var_pop: Option<LinksVarPopOrderBy>,
    #[graphql(name = "var_samp")]
    pub var_samp: Option<LinksVarSampOrderBy>,
    pub variance: Option<LinksVarianceOrderBy>,
}
