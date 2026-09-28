use crate::{
    filters::{Column, Filter, Ordering, Selection},
    store::RawStore,
    Database,
};
use async_graphql::{
    Context, EmptySubscription, InputObject, Object, Result, Schema, SimpleObject,
};
use doublets::{Doublets, DoubletsExt, Link};
use std::collections::{HashMap, HashSet};

pub type ApiSchema = Schema<QueryRoot, MutationRoot, EmptySubscription>;
pub fn build_schema(database: Database) -> ApiSchema {
    Schema::build(QueryRoot, MutationRoot, EmptySubscription)
        .data(database)
        .limit_depth(32)
        .limit_complexity(1000)
        .finish()
}
fn database<'a>(ctx: &'a Context<'_>) -> Result<&'a Database> {
    ctx.data::<Database>()
}
fn address(value: i64) -> std::result::Result<u64, String> {
    u64::try_from(value).map_err(|_| "Link addresses must be non-negative".into())
}
fn id(value: i64) -> std::result::Result<u64, String> {
    if value == 0 {
        return Err("Link IDs must be positive".into());
    }
    address(value)
}
fn snapshot(store: &RawStore) -> Vec<Link<u64>> {
    store.iter().collect()
}
fn object(link: Link<u64>) -> std::result::Result<LinkObject, String> {
    for n in [link.index, link.source, link.target] {
        i64::try_from(n).map_err(|_| "Stored address exceeds the GraphQL signed 64-bit range")?;
    }
    Ok(LinkObject(link))
}
fn objects(links: Vec<Link<u64>>) -> std::result::Result<Vec<LinkObject>, String> {
    links.into_iter().map(object).collect()
}
async fn select(
    ctx: &Context<'_>,
    selection: Selection,
    from: Option<u64>,
    to: Option<u64>,
) -> Result<Vec<LinkObject>> {
    selection.validate()?;
    database(ctx)?
        .execute(move |store| objects(selection.apply(&snapshot(store), from, to)))
        .await
}
async fn get(ctx: &Context<'_>, value: u64) -> Result<Option<LinkObject>> {
    database(ctx)?
        .execute(move |store| store.get_link(value).map(object).transpose())
        .await
}

#[derive(Clone)]
pub struct LinkObject(Link<u64>);
#[Object(name = "links")]
impl LinkObject {
    async fn id(&self) -> i64 {
        self.0.index as i64
    }
    #[graphql(name = "from_id")]
    async fn from_id(&self) -> i64 {
        self.0.source as i64
    }
    #[graphql(name = "to_id")]
    async fn to_id(&self) -> i64 {
        self.0.target as i64
    }
    async fn from(&self, ctx: &Context<'_>) -> Result<Option<LinkObject>> {
        get(ctx, self.0.source).await
    }
    async fn to(&self, ctx: &Context<'_>) -> Result<Option<LinkObject>> {
        get(ctx, self.0.target).await
    }
    #[allow(clippy::too_many_arguments)]
    async fn out(
        &self,
        ctx: &Context<'_>,
        #[graphql(name = "where")] filter: Option<Filter>,
        #[graphql(name = "order_by")] order: Option<Vec<Ordering>>,
        #[graphql(name = "distinct_on")] distinct: Option<Vec<Column>>,
        offset: Option<i32>,
        limit: Option<i32>,
    ) -> Result<Vec<LinkObject>> {
        select(
            ctx,
            Selection {
                filter,
                order,
                distinct,
                offset,
                limit,
            },
            Some(self.0.index),
            None,
        )
        .await
    }
    #[graphql(name = "in")]
    #[allow(clippy::too_many_arguments)]
    async fn incoming(
        &self,
        ctx: &Context<'_>,
        #[graphql(name = "where")] filter: Option<Filter>,
        #[graphql(name = "order_by")] order: Option<Vec<Ordering>>,
        #[graphql(name = "distinct_on")] distinct: Option<Vec<Column>>,
        offset: Option<i32>,
        limit: Option<i32>,
    ) -> Result<Vec<LinkObject>> {
        select(
            ctx,
            Selection {
                filter,
                order,
                distinct,
                offset,
                limit,
            },
            None,
            Some(self.0.index),
        )
        .await
    }
}

pub struct QueryRoot;
#[Object(name = "query_root")]
impl QueryRoot {
    #[allow(clippy::too_many_arguments)]
    async fn links(
        &self,
        ctx: &Context<'_>,
        #[graphql(name = "where")] filter: Option<Filter>,
        #[graphql(name = "order_by")] order: Option<Vec<Ordering>>,
        #[graphql(name = "distinct_on")] distinct: Option<Vec<Column>>,
        offset: Option<i32>,
        limit: Option<i32>,
    ) -> Result<Vec<LinkObject>> {
        select(
            ctx,
            Selection {
                filter,
                order,
                distinct,
                offset,
                limit,
            },
            None,
            None,
        )
        .await
    }
    #[graphql(name = "links_by_pk")]
    async fn links_by_pk(
        &self,
        ctx: &Context<'_>,
        #[graphql(name = "id")] value: i64,
    ) -> Result<Option<LinkObject>> {
        get(ctx, id(value)?).await
    }
}

#[derive(InputObject, Clone)]
#[graphql(name = "links_insert_input")]
pub struct Insert {
    #[graphql(name = "from_id")]
    pub from_id: Option<i64>,
    #[graphql(name = "to_id")]
    pub to_id: Option<i64>,
}
#[derive(InputObject, Clone, Default)]
#[graphql(name = "links_set_input")]
pub struct Set {
    #[graphql(name = "from_id")]
    pub from_id: Option<i64>,
    #[graphql(name = "to_id")]
    pub to_id: Option<i64>,
}
#[derive(InputObject, Clone, Default)]
#[graphql(name = "links_inc_input")]
pub struct Increment {
    #[graphql(name = "from_id")]
    pub from_id: Option<i64>,
    #[graphql(name = "to_id")]
    pub to_id: Option<i64>,
}
#[derive(InputObject)]
#[graphql(name = "links_pk_columns_input")]
pub struct PrimaryKey {
    pub id: i64,
}
#[derive(SimpleObject)]
#[graphql(name = "links_mutation_response")]
pub struct MutationResponse {
    #[graphql(name = "affected_rows")]
    affected_rows: i32,
    returning: Vec<LinkObject>,
}
impl MutationResponse {
    fn new(links: Vec<LinkObject>) -> Self {
        Self {
            affected_rows: links.len() as i32,
            returning: links,
        }
    }
}

fn insert(
    store: &mut RawStore,
    input: Vec<Insert>,
) -> std::result::Result<Vec<LinkObject>, String> {
    // Validate the entire batch before changing the store.
    let pairs: Vec<_> = input
        .into_iter()
        .map(|i| {
            Ok((
                address(i.from_id.unwrap_or(0))?,
                address(i.to_id.unwrap_or(0))?,
            ))
        })
        .collect::<std::result::Result<_, String>>()?;
    // Native (0,0) links are deliberately unindexed; use the same pair lookup
    // for every address combination so null-ended duplicates are idempotent too.
    let mut existing: HashMap<_, _> = snapshot(store)
        .into_iter()
        .map(|link| ((link.source, link.target), link.index))
        .collect();
    let new_pairs: HashSet<_> = pairs
        .iter()
        .filter(|pair| !existing.contains_key(pair))
        .collect();
    validate_capacity(
        existing.len(),
        new_pairs.len(),
        crate::storage_format::MAX_LINKS,
    )?;
    pairs
        .into_iter()
        .map(|(from, to)| {
            let id = if let Some(id) = existing.get(&(from, to)) {
                *id
            } else {
                let id = store.create_link(from, to).map_err(|e| e.to_string())?;
                existing.insert((from, to), id);
                id
            };
            store
                .get_link(id)
                .ok_or_else(|| "Inserted link is unavailable".to_string())
                .and_then(object)
        })
        .collect()
}
fn change(value: u64, set: Option<i64>, inc: Option<i64>) -> std::result::Result<u64, String> {
    if set.is_some() && inc.is_some() {
        return Err("Cannot set and increment the same column".into());
    }
    let value = if let Some(set) = set {
        address(set)?
    } else {
        value
    };
    let changed = i128::from(value) + i128::from(inc.unwrap_or(0));
    let bounded =
        i64::try_from(changed).map_err(|_| "Updated address is outside the signed 64-bit range")?;
    address(bounded)
}
fn update(
    store: &mut RawStore,
    filter: Filter,
    set: Option<Set>,
    inc: Option<Increment>,
) -> std::result::Result<Vec<LinkObject>, String> {
    filter.validate(0)?;
    let set = set.unwrap_or_default();
    let inc = inc.unwrap_or_default();
    if set.from_id.is_none() && set.to_id.is_none() && inc.from_id.is_none() && inc.to_id.is_none()
    {
        return Err("An update must set or increment from_id or to_id".into());
    }
    // Invalid values are rejected even if the filter matches no rows.
    change(0, set.from_id, None)?;
    change(0, set.to_id, None)?;
    if (set.from_id.is_some() && inc.from_id.is_some())
        || (set.to_id.is_some() && inc.to_id.is_some())
    {
        return Err("Cannot set and increment the same column".into());
    }
    let all = snapshot(store);
    let selected = Selection {
        filter: Some(filter),
        ..Default::default()
    }
    .apply(&all, None, None);
    let selected_ids: HashSet<_> = selected.iter().map(|l| l.index).collect();
    let mut plan = vec![];
    let mut seen: HashSet<_> = all
        .iter()
        .filter(|l| !selected_ids.contains(&l.index))
        .map(|l| (l.source, l.target))
        .collect();
    for l in selected {
        let from = change(l.source, set.from_id, inc.from_id)?;
        let to = change(l.target, set.to_id, inc.to_id)?;
        if !seen.insert((from, to)) {
            return Err("Update would create duplicate links; no changes applied".into());
        }
        plan.push((l.index, from, to));
    }
    // Detach selected pairs from the native indexes before applying their final
    // values. (0,0) is the native unindexed state, so swaps and bulk increments
    // cannot temporarily put duplicate keys into the search trees.
    for (id, _, _) in &plan {
        store.update(*id, 0, 0).map_err(|e| e.to_string())?;
    }
    plan.into_iter()
        .map(|(id, from, to)| {
            store.update(id, from, to).map_err(|e| e.to_string())?;
            store
                .get_link(id)
                .ok_or_else(|| "Updated link is unavailable".to_string())
                .and_then(object)
        })
        .collect()
}
fn delete(store: &mut RawStore, filter: Filter) -> std::result::Result<Vec<LinkObject>, String> {
    filter.validate(0)?;
    let selected = Selection {
        filter: Some(filter),
        ..Default::default()
    }
    .apply(&snapshot(store), None, None);
    let result = objects(selected.clone())?;
    for l in selected {
        store.delete(l.index).map_err(|e| e.to_string())?;
    }
    Ok(result)
}
fn by_id(value: i64) -> std::result::Result<Filter, String> {
    id(value)?;
    Ok(Filter {
        id: Some(crate::filters::Comparison {
            eq: Some(value),
            ..Default::default()
        }),
        ..Default::default()
    })
}

pub struct MutationRoot;
#[Object(name = "mutation_root")]
impl MutationRoot {
    #[graphql(name = "insert_links_one")]
    async fn insert_links_one(
        &self,
        ctx: &Context<'_>,
        object: Insert,
    ) -> Result<Option<LinkObject>> {
        database(ctx)?
            .execute(move |s| insert(s, vec![object]).map(|mut v| v.pop()))
            .await
    }
    #[graphql(name = "insert_links")]
    async fn insert_links(
        &self,
        ctx: &Context<'_>,
        objects: Vec<Insert>,
    ) -> Result<MutationResponse> {
        database(ctx)?
            .execute(move |s| insert(s, objects).map(MutationResponse::new))
            .await
    }
    #[graphql(name = "update_links")]
    async fn update_links(
        &self,
        ctx: &Context<'_>,
        #[graphql(name = "where")] filter: Filter,
        #[graphql(name = "_set")] set: Option<Set>,
        #[graphql(name = "_inc")] inc: Option<Increment>,
    ) -> Result<MutationResponse> {
        database(ctx)?
            .execute(move |s| update(s, filter, set, inc).map(MutationResponse::new))
            .await
    }
    #[graphql(name = "update_links_by_pk")]
    async fn update_links_by_pk(
        &self,
        ctx: &Context<'_>,
        #[graphql(name = "pk_columns")] key: PrimaryKey,
        #[graphql(name = "_set")] set: Option<Set>,
        #[graphql(name = "_inc")] inc: Option<Increment>,
    ) -> Result<Option<LinkObject>> {
        let filter = by_id(key.id)?;
        database(ctx)?
            .execute(move |s| update(s, filter, set, inc).map(|mut v| v.pop()))
            .await
    }
    #[graphql(name = "delete_links")]
    async fn delete_links(
        &self,
        ctx: &Context<'_>,
        #[graphql(name = "where")] filter: Filter,
    ) -> Result<MutationResponse> {
        database(ctx)?
            .execute(move |s| delete(s, filter).map(MutationResponse::new))
            .await
    }
    #[graphql(name = "delete_links_by_pk")]
    async fn delete_links_by_pk(
        &self,
        ctx: &Context<'_>,
        #[graphql(name = "id")] value: i64,
    ) -> Result<Option<LinkObject>> {
        let filter = by_id(value)?;
        database(ctx)?
            .execute(move |s| delete(s, filter).map(|mut v| v.pop()))
            .await
    }
}

fn validate_capacity(
    existing: usize,
    added: usize,
    maximum: usize,
) -> std::result::Result<(), String> {
    if added > maximum.saturating_sub(existing) {
        return Err(format!(
            "Database capacity is {maximum} links; no changes applied"
        ));
    }
    Ok(())
}

#[cfg(test)]
mod capacity_tests {
    use super::validate_capacity;
    #[test]
    fn capacity_accepts_duplicates_and_freed_slots_but_rejects_oversized_batch() {
        assert!(validate_capacity(10, 0, 10).is_ok());
        assert!(validate_capacity(9, 1, 10).is_ok());
        assert!(validate_capacity(9, 2, 10).is_err());
        assert!(validate_capacity(10, 1, 10).is_err());
    }
}
