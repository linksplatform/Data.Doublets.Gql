use crate::model::Bigint;
use crate::model::Can;
use crate::model::CanAggregate;
use crate::model::CanBoolExp;
use crate::model::CanOrderBy;
use crate::model::CanSelectColumn;
use crate::model::DownLinksArgs;
use crate::model::ExecBoolExpLinksArgs;
use crate::model::Jsonb;
use crate::model::LinksAggregate;
use crate::model::LinksBoolExp;
use crate::model::LinksOrderBy;
use crate::model::LinksSelectColumn;
use crate::model::Mp;
use crate::model::MpAggregate;
use crate::model::MpBoolExp;
use crate::model::MpOrderBy;
use crate::model::MpSelectColumn;
use crate::model::Numbers;
use crate::model::Objects;
use crate::model::Selectors;
use crate::model::SelectorsAggregate;
use crate::model::SelectorsBoolExp;
use crate::model::SelectorsOrderBy;
use crate::model::SelectorsSelectColumn;
use crate::model::Strings;
use crate::model::UpLinksArgs;
use crate::Store;
use async_graphql::*;
use doublets::Doublets;

#[derive(Debug, Clone)]
pub struct Links(pub doublets::Link<u64>);

#[Object(name = "links")]
impl Links {
    #[graphql(name = "_by_group")]
    pub async fn by_group(
        &self,
        ctx: &Context<'_>,
        #[graphql(name = "distinct_on")] distinct_on: Option<Vec<MpSelectColumn>>,
        limit: Option<i32>,
        offset: Option<i32>,
        #[graphql(name = "order_by")] order_by: Option<Vec<MpOrderBy>>,
        _where: Option<Box<MpBoolExp>>,
    ) -> Vec<Mp> {
        todo!()
    }
    #[graphql(name = "_by_group_aggregate")]
    pub async fn by_group_aggregate(
        &self,
        ctx: &Context<'_>,
        #[graphql(name = "distinct_on")] distinct_on: Option<Vec<MpSelectColumn>>,
        limit: Option<i32>,
        offset: Option<i32>,
        #[graphql(name = "order_by")] order_by: Option<Vec<MpOrderBy>>,
        _where: Option<Box<MpBoolExp>>,
    ) -> MpAggregate {
        todo!()
    }
    #[graphql(name = "_by_item")]
    pub async fn by_item(
        &self,
        ctx: &Context<'_>,
        #[graphql(name = "distinct_on")] distinct_on: Option<Vec<MpSelectColumn>>,
        limit: Option<i32>,
        offset: Option<i32>,
        #[graphql(name = "order_by")] order_by: Option<Vec<MpOrderBy>>,
        _where: Option<Box<MpBoolExp>>,
    ) -> Vec<Mp> {
        todo!()
    }
    #[graphql(name = "_by_item_aggregate")]
    pub async fn by_item_aggregate(
        &self,
        ctx: &Context<'_>,
        #[graphql(name = "distinct_on")] distinct_on: Option<Vec<MpSelectColumn>>,
        limit: Option<i32>,
        offset: Option<i32>,
        #[graphql(name = "order_by")] order_by: Option<Vec<MpOrderBy>>,
        _where: Option<Box<MpBoolExp>>,
    ) -> MpAggregate {
        todo!()
    }
    #[graphql(name = "_by_path_item")]
    pub async fn by_path_item(
        &self,
        ctx: &Context<'_>,
        #[graphql(name = "distinct_on")] distinct_on: Option<Vec<MpSelectColumn>>,
        limit: Option<i32>,
        offset: Option<i32>,
        #[graphql(name = "order_by")] order_by: Option<Vec<MpOrderBy>>,
        _where: Option<Box<MpBoolExp>>,
    ) -> Vec<Mp> {
        todo!()
    }
    #[graphql(name = "_by_path_item_aggregate")]
    pub async fn by_path_item_aggregate(
        &self,
        ctx: &Context<'_>,
        #[graphql(name = "distinct_on")] distinct_on: Option<Vec<MpSelectColumn>>,
        limit: Option<i32>,
        offset: Option<i32>,
        #[graphql(name = "order_by")] order_by: Option<Vec<MpOrderBy>>,
        _where: Option<Box<MpBoolExp>>,
    ) -> MpAggregate {
        todo!()
    }
    #[graphql(name = "_by_root")]
    pub async fn by_root(
        &self,
        ctx: &Context<'_>,
        #[graphql(name = "distinct_on")] distinct_on: Option<Vec<MpSelectColumn>>,
        limit: Option<i32>,
        offset: Option<i32>,
        #[graphql(name = "order_by")] order_by: Option<Vec<MpOrderBy>>,
        _where: Option<Box<MpBoolExp>>,
    ) -> Vec<Mp> {
        todo!()
    }
    #[graphql(name = "_by_root_aggregate")]
    pub async fn by_root_aggregate(
        &self,
        ctx: &Context<'_>,
        #[graphql(name = "distinct_on")] distinct_on: Option<Vec<MpSelectColumn>>,
        limit: Option<i32>,
        offset: Option<i32>,
        #[graphql(name = "order_by")] order_by: Option<Vec<MpOrderBy>>,
        _where: Option<Box<MpBoolExp>>,
    ) -> MpAggregate {
        todo!()
    }
    #[graphql(name = "can_action")]
    pub async fn can_action(
        &self,
        ctx: &Context<'_>,
        #[graphql(name = "distinct_on")] distinct_on: Option<Vec<CanSelectColumn>>,
        limit: Option<i32>,
        offset: Option<i32>,
        #[graphql(name = "order_by")] order_by: Option<Vec<CanOrderBy>>,
        _where: Option<Box<CanBoolExp>>,
    ) -> Vec<Can> {
        todo!()
    }
    #[graphql(name = "can_action_aggregate")]
    pub async fn can_action_aggregate(
        &self,
        ctx: &Context<'_>,
        #[graphql(name = "distinct_on")] distinct_on: Option<Vec<CanSelectColumn>>,
        limit: Option<i32>,
        offset: Option<i32>,
        #[graphql(name = "order_by")] order_by: Option<Vec<CanOrderBy>>,
        _where: Option<Box<CanBoolExp>>,
    ) -> CanAggregate {
        todo!()
    }
    #[graphql(name = "can_object")]
    pub async fn can_object(
        &self,
        ctx: &Context<'_>,
        #[graphql(name = "distinct_on")] distinct_on: Option<Vec<CanSelectColumn>>,
        limit: Option<i32>,
        offset: Option<i32>,
        #[graphql(name = "order_by")] order_by: Option<Vec<CanOrderBy>>,
        _where: Option<Box<CanBoolExp>>,
    ) -> Vec<Can> {
        todo!()
    }
    #[graphql(name = "can_object_aggregate")]
    pub async fn can_object_aggregate(
        &self,
        ctx: &Context<'_>,
        #[graphql(name = "distinct_on")] distinct_on: Option<Vec<CanSelectColumn>>,
        limit: Option<i32>,
        offset: Option<i32>,
        #[graphql(name = "order_by")] order_by: Option<Vec<CanOrderBy>>,
        _where: Option<Box<CanBoolExp>>,
    ) -> CanAggregate {
        todo!()
    }
    #[graphql(name = "can_rule")]
    pub async fn can_rule(
        &self,
        ctx: &Context<'_>,
        #[graphql(name = "distinct_on")] distinct_on: Option<Vec<CanSelectColumn>>,
        limit: Option<i32>,
        offset: Option<i32>,
        #[graphql(name = "order_by")] order_by: Option<Vec<CanOrderBy>>,
        _where: Option<Box<CanBoolExp>>,
    ) -> Vec<Can> {
        todo!()
    }
    #[graphql(name = "can_rule_aggregate")]
    pub async fn can_rule_aggregate(
        &self,
        ctx: &Context<'_>,
        #[graphql(name = "distinct_on")] distinct_on: Option<Vec<CanSelectColumn>>,
        limit: Option<i32>,
        offset: Option<i32>,
        #[graphql(name = "order_by")] order_by: Option<Vec<CanOrderBy>>,
        _where: Option<Box<CanBoolExp>>,
    ) -> CanAggregate {
        todo!()
    }
    #[graphql(name = "can_subject")]
    pub async fn can_subject(
        &self,
        ctx: &Context<'_>,
        #[graphql(name = "distinct_on")] distinct_on: Option<Vec<CanSelectColumn>>,
        limit: Option<i32>,
        offset: Option<i32>,
        #[graphql(name = "order_by")] order_by: Option<Vec<CanOrderBy>>,
        _where: Option<Box<CanBoolExp>>,
    ) -> Vec<Can> {
        todo!()
    }
    #[graphql(name = "can_subject_aggregate")]
    pub async fn can_subject_aggregate(
        &self,
        ctx: &Context<'_>,
        #[graphql(name = "distinct_on")] distinct_on: Option<Vec<CanSelectColumn>>,
        limit: Option<i32>,
        offset: Option<i32>,
        #[graphql(name = "order_by")] order_by: Option<Vec<CanOrderBy>>,
        _where: Option<Box<CanBoolExp>>,
    ) -> CanAggregate {
        todo!()
    }
    pub async fn down(
        &self,
        ctx: &Context<'_>,
        args: DownLinksArgs,
        #[graphql(name = "distinct_on")] distinct_on: Option<Vec<LinksSelectColumn>>,
        limit: Option<i32>,
        offset: Option<i32>,
        #[graphql(name = "order_by")] order_by: Option<Vec<LinksOrderBy>>,
        _where: Option<Box<LinksBoolExp>>,
    ) -> Option<Vec<Links>> {
        todo!()
    }
    #[graphql(name = "exec_bool_exp")]
    pub async fn exec_bool_exp(
        &self,
        ctx: &Context<'_>,
        args: ExecBoolExpLinksArgs,
        #[graphql(name = "distinct_on")] distinct_on: Option<Vec<LinksSelectColumn>>,
        limit: Option<i32>,
        offset: Option<i32>,
        #[graphql(name = "order_by")] order_by: Option<Vec<LinksOrderBy>>,
        _where: Option<Box<LinksBoolExp>>,
    ) -> Option<Vec<Links>> {
        todo!()
    }

    pub async fn from(&self, ctx: &Context<'_>) -> Option<Links> {
        let store = ctx.data_unchecked::<Store>().read().await;
        store.get_link(self.0.source).map(|link| Links(link))
    }

    #[graphql(name = "from_id")]
    pub async fn from_id(&self, ctx: &Context<'_>) -> Option<Bigint> {
        Some(self.0.source as Bigint)
    }

    pub async fn id(&self, ctx: &Context<'_>) -> Bigint {
        self.0.index as Bigint
    }

    pub async fn _in(
        &self,
        ctx: &Context<'_>,
        #[graphql(name = "distinct_on")] distinct_on: Option<Vec<LinksSelectColumn>>,
        limit: Option<i32>,
        offset: Option<i32>,
        #[graphql(name = "order_by")] order_by: Option<Vec<LinksOrderBy>>,
        _where: Option<Box<LinksBoolExp>>,
    ) -> Vec<Links> {
        todo!()
    }
    #[graphql(name = "in_aggregate")]
    pub async fn in_aggregate(
        &self,
        ctx: &Context<'_>,
        #[graphql(name = "distinct_on")] distinct_on: Option<Vec<LinksSelectColumn>>,
        limit: Option<i32>,
        offset: Option<i32>,
        #[graphql(name = "order_by")] order_by: Option<Vec<LinksOrderBy>>,
        _where: Option<Box<LinksBoolExp>>,
    ) -> LinksAggregate {
        todo!()
    }
    pub async fn number(&self, ctx: &Context<'_>) -> Option<Numbers> {
        todo!()
    }
    pub async fn object(&self, ctx: &Context<'_>) -> Option<Objects> {
        todo!()
    }
    pub async fn out(
        &self,
        ctx: &Context<'_>,
        #[graphql(name = "distinct_on")] distinct_on: Option<Vec<LinksSelectColumn>>,
        limit: Option<i32>,
        offset: Option<i32>,
        #[graphql(name = "order_by")] order_by: Option<Vec<LinksOrderBy>>,
        _where: Option<Box<LinksBoolExp>>,
    ) -> Vec<Links> {
        todo!()
    }
    #[graphql(name = "out_aggregate")]
    pub async fn out_aggregate(
        &self,
        ctx: &Context<'_>,
        #[graphql(name = "distinct_on")] distinct_on: Option<Vec<LinksSelectColumn>>,
        limit: Option<i32>,
        offset: Option<i32>,
        #[graphql(name = "order_by")] order_by: Option<Vec<LinksOrderBy>>,
        _where: Option<Box<LinksBoolExp>>,
    ) -> LinksAggregate {
        todo!()
    }
    pub async fn selected(
        &self,
        ctx: &Context<'_>,
        #[graphql(name = "distinct_on")] distinct_on: Option<Vec<SelectorsSelectColumn>>,
        limit: Option<i32>,
        offset: Option<i32>,
        #[graphql(name = "order_by")] order_by: Option<Vec<SelectorsOrderBy>>,
        _where: Option<SelectorsBoolExp>,
    ) -> Vec<Selectors> {
        todo!()
    }
    #[graphql(name = "selected_aggregate")]
    pub async fn selected_aggregate(
        &self,
        ctx: &Context<'_>,
        #[graphql(name = "distinct_on")] distinct_on: Option<Vec<SelectorsSelectColumn>>,
        limit: Option<i32>,
        offset: Option<i32>,
        #[graphql(name = "order_by")] order_by: Option<Vec<SelectorsOrderBy>>,
        _where: Option<SelectorsBoolExp>,
    ) -> SelectorsAggregate {
        todo!()
    }
    pub async fn selectors(
        &self,
        ctx: &Context<'_>,
        #[graphql(name = "distinct_on")] distinct_on: Option<Vec<SelectorsSelectColumn>>,
        limit: Option<i32>,
        offset: Option<i32>,
        #[graphql(name = "order_by")] order_by: Option<Vec<SelectorsOrderBy>>,
        _where: Option<SelectorsBoolExp>,
    ) -> Vec<Selectors> {
        todo!()
    }
    #[graphql(name = "selectors_aggregate")]
    pub async fn selectors_aggregate(
        &self,
        ctx: &Context<'_>,
        #[graphql(name = "distinct_on")] distinct_on: Option<Vec<SelectorsSelectColumn>>,
        limit: Option<i32>,
        offset: Option<i32>,
        #[graphql(name = "order_by")] order_by: Option<Vec<SelectorsOrderBy>>,
        _where: Option<SelectorsBoolExp>,
    ) -> SelectorsAggregate {
        todo!()
    }
    pub async fn string(&self, ctx: &Context<'_>) -> Option<Strings> {
        todo!()
    }

    pub async fn to(&self, ctx: &Context<'_>) -> Option<Links> {
        let store = ctx.data_unchecked::<Store>().read().await;
        store.get_link(self.0.target).map(|link| Links(link))
    }

    #[graphql(name = "to_id")]
    pub async fn to_id(&self, ctx: &Context<'_>) -> Option<Bigint> {
        Some(self.0.target as Bigint)
    }

    pub async fn _type(&self, ctx: &Context<'_>) -> Option<Links> {
        todo!()
    }
    #[graphql(name = "type_id")]
    pub async fn type_id(&self, ctx: &Context<'_>) -> Bigint {
        todo!()
    }
    pub async fn typed(
        &self,
        ctx: &Context<'_>,
        #[graphql(name = "distinct_on")] distinct_on: Option<Vec<LinksSelectColumn>>,
        limit: Option<i32>,
        offset: Option<i32>,
        #[graphql(name = "order_by")] order_by: Option<Vec<LinksOrderBy>>,
        _where: Option<Box<LinksBoolExp>>,
    ) -> Vec<Links> {
        todo!()
    }
    #[graphql(name = "typed_aggregate")]
    pub async fn typed_aggregate(
        &self,
        ctx: &Context<'_>,
        #[graphql(name = "distinct_on")] distinct_on: Option<Vec<LinksSelectColumn>>,
        limit: Option<i32>,
        offset: Option<i32>,
        #[graphql(name = "order_by")] order_by: Option<Vec<LinksOrderBy>>,
        _where: Option<Box<LinksBoolExp>>,
    ) -> LinksAggregate {
        todo!()
    }
    pub async fn up(
        &self,
        ctx: &Context<'_>,
        args: UpLinksArgs,
        #[graphql(name = "distinct_on")] distinct_on: Option<Vec<LinksSelectColumn>>,
        limit: Option<i32>,
        offset: Option<i32>,
        #[graphql(name = "order_by")] order_by: Option<Vec<LinksOrderBy>>,
        _where: Option<Box<LinksBoolExp>>,
    ) -> Option<Vec<Links>> {
        todo!()
    }
    pub async fn value(&self, ctx: &Context<'_>, path: Option<String>) -> Option<Jsonb> {
        todo!()
    }
}
