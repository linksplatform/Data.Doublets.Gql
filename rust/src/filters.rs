use async_graphql::{Enum, InputObject};
use doublets::Link;
use std::collections::{HashMap, HashSet};

#[derive(InputObject, Clone, Default)]
#[graphql(name = "bigint_comparison_exp")]
pub struct Comparison {
    #[graphql(name = "_eq")]
    pub eq: Option<i64>,
    #[graphql(name = "_neq")]
    pub neq: Option<i64>,
    #[graphql(name = "_gt")]
    pub gt: Option<i64>,
    #[graphql(name = "_gte")]
    pub gte: Option<i64>,
    #[graphql(name = "_lt")]
    pub lt: Option<i64>,
    #[graphql(name = "_lte")]
    pub lte: Option<i64>,
    #[graphql(name = "_in")]
    pub inside: Option<Vec<i64>>,
    #[graphql(name = "_nin")]
    pub outside: Option<Vec<i64>>,
    #[graphql(name = "_is_null")]
    pub is_null: Option<bool>,
}
impl Comparison {
    fn matches(&self, n: u64) -> bool {
        // Compare as i128 so negative input never wraps into an unsigned ID.
        let n = i128::from(n);
        self.eq.is_none_or(|v| n == i128::from(v))
            && self.neq.is_none_or(|v| n != i128::from(v))
            && self.gt.is_none_or(|v| n > i128::from(v))
            && self.gte.is_none_or(|v| n >= i128::from(v))
            && self.lt.is_none_or(|v| n < i128::from(v))
            && self.lte.is_none_or(|v| n <= i128::from(v))
            && self
                .inside
                .as_ref()
                .is_none_or(|v| v.iter().any(|x| n == i128::from(*x)))
            && self
                .outside
                .as_ref()
                .is_none_or(|v| v.iter().all(|x| n != i128::from(*x)))
            && self.is_null.is_none_or(|v| (n == 0) == v)
    }
}

#[derive(InputObject, Clone, Default)]
#[graphql(name = "links_bool_exp")]
pub struct Filter {
    pub id: Option<Comparison>,
    #[graphql(name = "from_id")]
    pub from_id: Option<Comparison>,
    #[graphql(name = "to_id")]
    pub to_id: Option<Comparison>,
    #[graphql(name = "_and")]
    pub and: Option<Vec<Filter>>,
    #[graphql(name = "_or")]
    pub or: Option<Vec<Filter>>,
    #[graphql(name = "_not")]
    pub not: Option<Box<Filter>>,
    pub from: Option<Box<Filter>>,
    pub to: Option<Box<Filter>>,
    #[graphql(name = "in")]
    pub incoming: Option<Box<Filter>>,
    pub out: Option<Box<Filter>>,
}
impl Filter {
    pub fn validate(&self, depth: usize) -> Result<(), String> {
        if depth > 32 {
            return Err("Filter nesting exceeds 32 levels".into());
        }
        for f in [&self.not, &self.from, &self.to, &self.incoming, &self.out]
            .into_iter()
            .flatten()
        {
            f.validate(depth + 1)?;
        }
        for group in [&self.and, &self.or].into_iter().flatten() {
            for f in group {
                f.validate(depth + 1)?;
            }
        }
        Ok(())
    }
    pub fn matches(
        &self,
        link: &Link<u64>,
        all: &[Link<u64>],
        by_id: &HashMap<u64, Link<u64>>,
    ) -> bool {
        self.id.as_ref().is_none_or(|f| f.matches(link.index))
            && self.from_id.as_ref().is_none_or(|f| f.matches(link.source))
            && self.to_id.as_ref().is_none_or(|f| f.matches(link.target))
            && self
                .and
                .as_ref()
                .is_none_or(|fs| fs.iter().all(|f| f.matches(link, all, by_id)))
            && self
                .or
                .as_ref()
                .is_none_or(|fs| fs.iter().any(|f| f.matches(link, all, by_id)))
            && self
                .not
                .as_ref()
                .is_none_or(|f| !f.matches(link, all, by_id))
            && self.from.as_ref().is_none_or(|f| {
                by_id
                    .get(&link.source)
                    .is_some_and(|l| f.matches(l, all, by_id))
            })
            && self.to.as_ref().is_none_or(|f| {
                by_id
                    .get(&link.target)
                    .is_some_and(|l| f.matches(l, all, by_id))
            })
            && self.incoming.as_ref().is_none_or(|f| {
                all.iter()
                    .any(|l| l.target == link.index && f.matches(l, all, by_id))
            })
            && self.out.as_ref().is_none_or(|f| {
                all.iter()
                    .any(|l| l.source == link.index && f.matches(l, all, by_id))
            })
    }
}

#[derive(Enum, Copy, Clone, Eq, PartialEq)]
#[graphql(name = "order_by", rename_items = "snake_case")]
pub enum Order {
    Asc,
    Desc,
}
#[derive(Enum, Copy, Clone, Eq, PartialEq)]
#[graphql(name = "links_select_column", rename_items = "snake_case")]
pub enum Column {
    Id,
    FromId,
    ToId,
}
impl Column {
    fn value(self, link: &Link<u64>) -> u64 {
        match self {
            Self::Id => link.index,
            Self::FromId => link.source,
            Self::ToId => link.target,
        }
    }
}
#[derive(InputObject, Clone, Default)]
#[graphql(name = "links_order_by")]
pub struct Ordering {
    pub id: Option<Order>,
    #[graphql(name = "from_id")]
    pub from_id: Option<Order>,
    #[graphql(name = "to_id")]
    pub to_id: Option<Order>,
}

#[derive(Default)]
pub(crate) struct Selection {
    pub filter: Option<Filter>,
    pub order: Option<Vec<Ordering>>,
    pub distinct: Option<Vec<Column>>,
    pub offset: Option<i32>,
    pub limit: Option<i32>,
}
impl Selection {
    pub fn validate(&self) -> Result<(), String> {
        if self.offset.is_some_and(|n| n < 0) || self.limit.is_some_and(|n| n < 0) {
            return Err("offset and limit must be non-negative".into());
        }
        if let Some(f) = &self.filter {
            f.validate(0)?;
        }
        Ok(())
    }
    pub fn apply(&self, all: &[Link<u64>], from: Option<u64>, to: Option<u64>) -> Vec<Link<u64>> {
        let by_id: HashMap<_, _> = all.iter().map(|l| (l.index, l.clone())).collect();
        let mut result: Vec<_> = all
            .iter()
            .filter(|l| {
                from.is_none_or(|id| l.source == id)
                    && to.is_none_or(|id| l.target == id)
                    && self
                        .filter
                        .as_ref()
                        .is_none_or(|f| f.matches(l, all, &by_id))
            })
            .cloned()
            .collect();
        let mut columns = vec![];
        for o in self.order.iter().flatten() {
            for (c, d) in [
                (Column::Id, o.id),
                (Column::FromId, o.from_id),
                (Column::ToId, o.to_id),
            ] {
                if let Some(d) = d {
                    columns.push((c, d));
                }
            }
        }
        result.sort_by(|a, b| {
            for (c, d) in &columns {
                let ordering = c.value(a).cmp(&c.value(b));
                if !ordering.is_eq() {
                    return if *d == Order::Asc {
                        ordering
                    } else {
                        ordering.reverse()
                    };
                }
            }
            a.index.cmp(&b.index)
        });
        if let Some(columns) = &self.distinct {
            if !columns.is_empty() {
                let mut seen = HashSet::new();
                result.retain(|l| {
                    seen.insert(columns.iter().map(|c| c.value(l)).collect::<Vec<_>>())
                });
            }
        }
        result
            .into_iter()
            .skip(self.offset.unwrap_or(0) as usize)
            .take(self.limit.map_or(usize::MAX, |n| n as usize))
            .collect()
    }
}
