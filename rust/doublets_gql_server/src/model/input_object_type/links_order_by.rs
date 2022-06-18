use crate::model::CanAggregateOrderBy;
use crate::model::LinksAggregateOrderBy;
use crate::model::MpAggregateOrderBy;
use crate::model::NumbersOrderBy;
use crate::model::ObjectsOrderBy;
use crate::model::OrderBy;
use crate::model::SelectorsAggregateOrderBy;
use crate::model::StringsOrderBy;
use async_graphql::*;
#[derive(InputObject, Debug)]
#[graphql(name = "links_order_by")]
pub struct LinksOrderBy {
    #[graphql(name = "_by_group_aggregate")]
    pub by_group_aggregate: Option<MpAggregateOrderBy>,
    #[graphql(name = "_by_item_aggregate")]
    pub by_item_aggregate: Option<MpAggregateOrderBy>,
    #[graphql(name = "_by_path_item_aggregate")]
    pub by_path_item_aggregate: Option<MpAggregateOrderBy>,
    #[graphql(name = "_by_root_aggregate")]
    pub by_root_aggregate: Option<MpAggregateOrderBy>,
    #[graphql(name = "can_action_aggregate")]
    pub can_action_aggregate: Option<CanAggregateOrderBy>,
    #[graphql(name = "can_object_aggregate")]
    pub can_object_aggregate: Option<CanAggregateOrderBy>,
    #[graphql(name = "can_rule_aggregate")]
    pub can_rule_aggregate: Option<CanAggregateOrderBy>,
    #[graphql(name = "can_subject_aggregate")]
    pub can_subject_aggregate: Option<CanAggregateOrderBy>,
    pub from: Option<Box<LinksOrderBy>>,
    #[graphql(name = "from_id")]
    pub from_id: Option<OrderBy>,
    pub id: Option<OrderBy>,
    #[graphql(name = "in_aggregate")]
    pub in_aggregate: Option<LinksAggregateOrderBy>,
    pub number: Option<Box<NumbersOrderBy>>,
    pub object: Option<Box<ObjectsOrderBy>>,
    #[graphql(name = "out_aggregate")]
    pub out_aggregate: Option<LinksAggregateOrderBy>,
    #[graphql(name = "selected_aggregate")]
    pub selected_aggregate: Option<SelectorsAggregateOrderBy>,
    #[graphql(name = "selectors_aggregate")]
    pub selectors_aggregate: Option<SelectorsAggregateOrderBy>,
    pub string: Option<Box<StringsOrderBy>>,
    pub to: Option<Box<LinksOrderBy>>,
    #[graphql(name = "to_id")]
    pub to_id: Option<OrderBy>,
    pub _type: Option<Box<LinksOrderBy>>,
    #[graphql(name = "type_id")]
    pub type_id: Option<OrderBy>,
    #[graphql(name = "typed_aggregate")]
    pub typed_aggregate: Option<LinksAggregateOrderBy>,
    pub value: Option<OrderBy>,
}
