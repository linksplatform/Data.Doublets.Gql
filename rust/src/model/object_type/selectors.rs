use crate::model::Bigint;
use crate::model::Links;
use crate::model::LinksAggregate;
use crate::model::LinksBoolExp;
use crate::model::LinksOrderBy;
use crate::model::LinksSelectColumn;
use async_graphql::*;

#[derive(Debug)]
pub struct Selectors;

#[Object(name = "selectors")]
impl Selectors {
    #[graphql(name = "bool_exp")]
    pub async fn bool_exp(
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
    #[graphql(name = "bool_exp_aggregate")]
    pub async fn bool_exp_aggregate(
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
    #[graphql(name = "bool_exp_id")]
    pub async fn bool_exp_id(&self, ctx: &Context<'_>) -> Option<Bigint> {
        todo!()
    }
    pub async fn item(&self, ctx: &Context<'_>) -> Option<Links> {
        todo!()
    }
    #[graphql(name = "item_id")]
    pub async fn item_id(&self, ctx: &Context<'_>) -> Option<Bigint> {
        todo!()
    }
    pub async fn selector(&self, ctx: &Context<'_>) -> Option<Links> {
        todo!()
    }
    #[graphql(name = "selector_id")]
    pub async fn selector_id(&self, ctx: &Context<'_>) -> Option<Bigint> {
        todo!()
    }
    #[graphql(name = "selector_include_id")]
    pub async fn selector_include_id(&self, ctx: &Context<'_>) -> Option<Bigint> {
        todo!()
    }
}
