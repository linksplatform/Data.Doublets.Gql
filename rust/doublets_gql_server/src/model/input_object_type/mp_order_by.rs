use crate::model::LinksOrderBy;
use crate::model::MpAggregateOrderBy;
use crate::model::OrderBy;
use async_graphql::*;
#[derive(InputObject, Debug)]
#[graphql(name = "mp_order_by")]
pub struct MpOrderBy {
    #[graphql(name = "by_group")]
    pub by_group: Option<LinksOrderBy>,
    #[graphql(name = "by_item_aggregate")]
    pub by_item_aggregate: Option<MpAggregateOrderBy>,
    #[graphql(name = "by_path_item_aggregate")]
    pub by_path_item_aggregate: Option<MpAggregateOrderBy>,
    #[graphql(name = "by_position_aggregate")]
    pub by_position_aggregate: Option<MpAggregateOrderBy>,
    #[graphql(name = "by_root_aggregate")]
    pub by_root_aggregate: Option<MpAggregateOrderBy>,
    #[graphql(name = "group_id")]
    pub group_id: Option<OrderBy>,
    pub id: Option<OrderBy>,
    #[graphql(name = "insert_category")]
    pub insert_category: Option<OrderBy>,
    pub item: Option<LinksOrderBy>,
    #[graphql(name = "item_id")]
    pub item_id: Option<OrderBy>,
    #[graphql(name = "path_item")]
    pub path_item: Option<LinksOrderBy>,
    #[graphql(name = "path_item_depth")]
    pub path_item_depth: Option<OrderBy>,
    #[graphql(name = "path_item_id")]
    pub path_item_id: Option<OrderBy>,
    #[graphql(name = "position_id")]
    pub position_id: Option<OrderBy>,
    pub root: Option<LinksOrderBy>,
    #[graphql(name = "root_id")]
    pub root_id: Option<OrderBy>,
}
