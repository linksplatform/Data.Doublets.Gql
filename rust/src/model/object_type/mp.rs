use crate::model::Bigint;
use crate::model::Links;
use crate::model::MpAggregate;
use crate::model::MpBoolExp;
use crate::model::MpOrderBy;
use crate::model::MpSelectColumn;
use async_graphql::*;
#[derive(Debug)]
pub struct Mp;
#[Object(name = "mp")]
impl Mp {
    #[graphql(name = "by_group")]
    pub async fn by_group(&self, ctx: &Context<'_>) -> Option<Links> {
        todo!()
    }
    #[graphql(name = "by_item")]
    pub async fn by_item(
        &self,
        ctx: &Context<'_>,
        #[graphql(name = "distinct_on")] distinct_on: Option<Vec<MpSelectColumn>>,
        limit: Option<i32>,
        offset: Option<i32>,
        #[graphql(name = "order_by")] order_by: Option<Vec<MpOrderBy>>,
        _where: Option<MpBoolExp>,
    ) -> Vec<Mp> {
        todo!()
    }
    #[graphql(name = "by_item_aggregate")]
    pub async fn by_item_aggregate(
        &self,
        ctx: &Context<'_>,
        #[graphql(name = "distinct_on")] distinct_on: Option<Vec<MpSelectColumn>>,
        limit: Option<i32>,
        offset: Option<i32>,
        #[graphql(name = "order_by")] order_by: Option<Vec<MpOrderBy>>,
        _where: Option<MpBoolExp>,
    ) -> MpAggregate {
        todo!()
    }
    #[graphql(name = "by_path_item")]
    pub async fn by_path_item(
        &self,
        ctx: &Context<'_>,
        #[graphql(name = "distinct_on")] distinct_on: Option<Vec<MpSelectColumn>>,
        limit: Option<i32>,
        offset: Option<i32>,
        #[graphql(name = "order_by")] order_by: Option<Vec<MpOrderBy>>,
        _where: Option<MpBoolExp>,
    ) -> Vec<Mp> {
        todo!()
    }
    #[graphql(name = "by_path_item_aggregate")]
    pub async fn by_path_item_aggregate(
        &self,
        ctx: &Context<'_>,
        #[graphql(name = "distinct_on")] distinct_on: Option<Vec<MpSelectColumn>>,
        limit: Option<i32>,
        offset: Option<i32>,
        #[graphql(name = "order_by")] order_by: Option<Vec<MpOrderBy>>,
        _where: Option<MpBoolExp>,
    ) -> MpAggregate {
        todo!()
    }
    #[graphql(name = "by_position")]
    pub async fn by_position(
        &self,
        ctx: &Context<'_>,
        #[graphql(name = "distinct_on")] distinct_on: Option<Vec<MpSelectColumn>>,
        limit: Option<i32>,
        offset: Option<i32>,
        #[graphql(name = "order_by")] order_by: Option<Vec<MpOrderBy>>,
        _where: Option<MpBoolExp>,
    ) -> Vec<Mp> {
        todo!()
    }
    #[graphql(name = "by_position_aggregate")]
    pub async fn by_position_aggregate(
        &self,
        ctx: &Context<'_>,
        #[graphql(name = "distinct_on")] distinct_on: Option<Vec<MpSelectColumn>>,
        limit: Option<i32>,
        offset: Option<i32>,
        #[graphql(name = "order_by")] order_by: Option<Vec<MpOrderBy>>,
        _where: Option<MpBoolExp>,
    ) -> MpAggregate {
        todo!()
    }
    #[graphql(name = "by_root")]
    pub async fn by_root(
        &self,
        ctx: &Context<'_>,
        #[graphql(name = "distinct_on")] distinct_on: Option<Vec<MpSelectColumn>>,
        limit: Option<i32>,
        offset: Option<i32>,
        #[graphql(name = "order_by")] order_by: Option<Vec<MpOrderBy>>,
        _where: Option<MpBoolExp>,
    ) -> Vec<Mp> {
        todo!()
    }
    #[graphql(name = "by_root_aggregate")]
    pub async fn by_root_aggregate(
        &self,
        ctx: &Context<'_>,
        #[graphql(name = "distinct_on")] distinct_on: Option<Vec<MpSelectColumn>>,
        limit: Option<i32>,
        offset: Option<i32>,
        #[graphql(name = "order_by")] order_by: Option<Vec<MpOrderBy>>,
        _where: Option<MpBoolExp>,
    ) -> MpAggregate {
        todo!()
    }
    #[graphql(name = "group_id")]
    pub async fn group_id(&self, ctx: &Context<'_>) -> Option<Bigint> {
        todo!()
    }
    pub async fn id(&self, ctx: &Context<'_>) -> Bigint {
        todo!()
    }
    #[graphql(name = "insert_category")]
    pub async fn insert_category(&self, ctx: &Context<'_>) -> Option<String> {
        todo!()
    }
    pub async fn item(&self, ctx: &Context<'_>) -> Option<Links> {
        todo!()
    }
    #[graphql(name = "item_id")]
    pub async fn item_id(&self, ctx: &Context<'_>) -> Option<Bigint> {
        todo!()
    }
    #[graphql(name = "path_item")]
    pub async fn path_item(&self, ctx: &Context<'_>) -> Option<Links> {
        todo!()
    }
    #[graphql(name = "path_item_depth")]
    pub async fn path_item_depth(&self, ctx: &Context<'_>) -> Option<Bigint> {
        todo!()
    }
    #[graphql(name = "path_item_id")]
    pub async fn path_item_id(&self, ctx: &Context<'_>) -> Option<Bigint> {
        todo!()
    }
    #[graphql(name = "position_id")]
    pub async fn position_id(&self, ctx: &Context<'_>) -> Option<String> {
        todo!()
    }
    pub async fn root(&self, ctx: &Context<'_>) -> Option<Links> {
        todo!()
    }
    #[graphql(name = "root_id")]
    pub async fn root_id(&self, ctx: &Context<'_>) -> Option<Bigint> {
        todo!()
    }
}
