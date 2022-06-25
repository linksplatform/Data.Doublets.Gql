use crate::model::Can;
use crate::model::CanAggregate;
use crate::model::CanBoolExp;
use crate::model::CanOrderBy;
use crate::model::CanSelectColumn;
use crate::model::GuestOutput;
use crate::model::JwtInput;
use crate::model::JwtOutput;
use crate::model::Links;
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
use crate::model::NumbersAggregate;
use crate::model::NumbersBoolExp;
use crate::model::NumbersOrderBy;
use crate::model::NumbersSelectColumn;
use crate::model::Objects;
use crate::model::ObjectsAggregate;
use crate::model::ObjectsBoolExp;
use crate::model::ObjectsOrderBy;
use crate::model::ObjectsSelectColumn;
use crate::model::Selectors;
use crate::model::SelectorsAggregate;
use crate::model::SelectorsBoolExp;
use crate::model::SelectorsOrderBy;
use crate::model::SelectorsSelectColumn;
use crate::model::Strings;
use crate::model::StringsAggregate;
use crate::model::StringsBoolExp;
use crate::model::StringsOrderBy;
use crate::model::StringsSelectColumn;
use crate::model::{Bigint, BigintComparisonExp, LinkType};
use crate::Store;
use async_graphql::*;
use doublets::Doublets;

#[derive(Debug)]
pub struct QueryRoot;

#[Object(name = "query_root")]
impl QueryRoot {
    pub async fn can(
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
    #[graphql(name = "can_aggregate")]
    pub async fn can_aggregate(
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
    pub async fn guest(&self, ctx: &Context<'_>) -> Option<GuestOutput> {
        todo!()
    }
    pub async fn jwt(&self, ctx: &Context<'_>, input: Option<JwtInput>) -> Option<JwtOutput> {
        todo!()
    }

    pub async fn links(
        &self,
        ctx: &Context<'_>,
        #[graphql(name = "distinct_on")] distinct_on: Option<Vec<LinksSelectColumn>>,
        limit: Option<i32>,
        offset: Option<i32>,
        #[graphql(name = "order_by")] order_by: Option<Vec<LinksOrderBy>>,
        _where: Option<Box<LinksBoolExp>>,
    ) -> Vec<Links> {
        let store = ctx.data_unchecked::<Store>().read().await;

        let fast_param_impl = |param: Option<&BigintComparisonExp>| -> LinkType {
            let any = store.constants().any;
            if let Some(param) = param {
                match param.eq {
                    Some(id) => id as LinkType,
                    None => any,
                }
            } else {
                any
            }
        };

        if let Some(r#where) = _where {
            let id = fast_param_impl(r#where.id.as_deref());
            let from_id = fast_param_impl(r#where.from_id.as_deref());
            let to_id = fast_param_impl(r#where.to_id.as_deref());

            store
                .each_iter([id, from_id, to_id])
                .filter(|link| r#where.matches(&*store, link))
                .map(|link| Links(link))
                .collect()
        } else {
            store.iter().map(|link| Links(link)).collect()
        }
    }

    #[graphql(name = "links_aggregate")]
    pub async fn links_aggregate(
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
    #[graphql(name = "links_by_pk")]
    pub async fn links_by_pk(&self, ctx: &Context<'_>, id: Bigint) -> Option<Links> {
        todo!()
    }
    pub async fn mp(
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
    #[graphql(name = "mp_aggregate")]
    pub async fn mp_aggregate(
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
    #[graphql(name = "mp_by_pk")]
    pub async fn mp_by_pk(&self, ctx: &Context<'_>, id: Bigint) -> Option<Mp> {
        todo!()
    }
    pub async fn numbers(
        &self,
        ctx: &Context<'_>,
        #[graphql(name = "distinct_on")] distinct_on: Option<Vec<NumbersSelectColumn>>,
        limit: Option<i32>,
        offset: Option<i32>,
        #[graphql(name = "order_by")] order_by: Option<Vec<NumbersOrderBy>>,
        _where: Option<NumbersBoolExp>,
    ) -> Vec<Numbers> {
        todo!()
    }
    #[graphql(name = "numbers_aggregate")]
    pub async fn numbers_aggregate(
        &self,
        ctx: &Context<'_>,
        #[graphql(name = "distinct_on")] distinct_on: Option<Vec<NumbersSelectColumn>>,
        limit: Option<i32>,
        offset: Option<i32>,
        #[graphql(name = "order_by")] order_by: Option<Vec<NumbersOrderBy>>,
        _where: Option<NumbersBoolExp>,
    ) -> NumbersAggregate {
        todo!()
    }
    #[graphql(name = "numbers_by_pk")]
    pub async fn numbers_by_pk(&self, ctx: &Context<'_>, id: Bigint) -> Option<Numbers> {
        todo!()
    }
    pub async fn objects(
        &self,
        ctx: &Context<'_>,
        #[graphql(name = "distinct_on")] distinct_on: Option<Vec<ObjectsSelectColumn>>,
        limit: Option<i32>,
        offset: Option<i32>,
        #[graphql(name = "order_by")] order_by: Option<Vec<ObjectsOrderBy>>,
        _where: Option<ObjectsBoolExp>,
    ) -> Vec<Objects> {
        todo!()
    }
    #[graphql(name = "objects_aggregate")]
    pub async fn objects_aggregate(
        &self,
        ctx: &Context<'_>,
        #[graphql(name = "distinct_on")] distinct_on: Option<Vec<ObjectsSelectColumn>>,
        limit: Option<i32>,
        offset: Option<i32>,
        #[graphql(name = "order_by")] order_by: Option<Vec<ObjectsOrderBy>>,
        _where: Option<ObjectsBoolExp>,
    ) -> ObjectsAggregate {
        todo!()
    }
    #[graphql(name = "objects_by_pk")]
    pub async fn objects_by_pk(&self, ctx: &Context<'_>, id: Bigint) -> Option<Objects> {
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
    pub async fn strings(
        &self,
        ctx: &Context<'_>,
        #[graphql(name = "distinct_on")] distinct_on: Option<Vec<StringsSelectColumn>>,
        limit: Option<i32>,
        offset: Option<i32>,
        #[graphql(name = "order_by")] order_by: Option<Vec<StringsOrderBy>>,
        _where: Option<StringsBoolExp>,
    ) -> Vec<Strings> {
        todo!()
    }
    #[graphql(name = "strings_aggregate")]
    pub async fn strings_aggregate(
        &self,
        ctx: &Context<'_>,
        #[graphql(name = "distinct_on")] distinct_on: Option<Vec<StringsSelectColumn>>,
        limit: Option<i32>,
        offset: Option<i32>,
        #[graphql(name = "order_by")] order_by: Option<Vec<StringsOrderBy>>,
        _where: Option<StringsBoolExp>,
    ) -> StringsAggregate {
        todo!()
    }
    #[graphql(name = "strings_by_pk")]
    pub async fn strings_by_pk(&self, ctx: &Context<'_>, id: Bigint) -> Option<Strings> {
        todo!()
    }
}
