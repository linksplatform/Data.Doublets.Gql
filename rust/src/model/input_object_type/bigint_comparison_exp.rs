use crate::model::{Bigint, LinkType};
use crate::RawStore;
use async_graphql::*;
use doublets::Doublets;

#[derive(InputObject, Debug)]
#[graphql(name = "bigint_comparison_exp")]
pub struct BigintComparisonExp {
    #[graphql(name = "_eq")]
    pub eq: Option<Bigint>,
    #[graphql(name = "_gt")]
    pub gt: Option<Bigint>,
    #[graphql(name = "_gte")]
    pub gte: Option<Bigint>,
    #[graphql(name = "_in")]
    pub _in: Option<Vec<Bigint>>,
    #[graphql(name = "_is_null")]
    pub is_null: Option<bool>,
    #[graphql(name = "_lt")]
    pub lt: Option<Bigint>,
    #[graphql(name = "_lte")]
    pub lte: Option<Bigint>,
    #[graphql(name = "_neq")]
    pub neq: Option<Bigint>,
    #[graphql(name = "_nin")]
    pub nin: Option<Vec<Bigint>>,
}

enum CmpExp {
    Equal,
    NotEqual,
    Great,
    GreatEqual,
    Less,
    LessEqual,
}

fn cmp(exp: CmpExp, a: LinkType, b: LinkType) -> bool {
    match exp {
        CmpExp::Equal => a == b,
        CmpExp::NotEqual => a != b,
        CmpExp::Great => a > b,
        CmpExp::GreatEqual => a >= b,
        CmpExp::Less => a < b,
        CmpExp::LessEqual => a <= b,
    }
}

impl BigintComparisonExp {
    fn matches_impl(exp: CmpExp, link: LinkType, target: Option<Bigint>) -> bool {
        if let Some(target) = target {
            cmp(exp, link, target as LinkType)
        } else {
            true
        }
    }

    pub fn matches(&self, link: LinkType) -> bool {
        let mut exp = Self::matches_impl(CmpExp::Equal, link, self.eq)
            && Self::matches_impl(CmpExp::NotEqual, link, self.neq)
            && Self::matches_impl(CmpExp::Great, link, self.gt)
            && Self::matches_impl(CmpExp::GreatEqual, link, self.gte)
            && Self::matches_impl(CmpExp::Less, link, self.lt)
            && Self::matches_impl(CmpExp::LessEqual, link, self.lte);

        match self.is_null {
            Some(true) => exp = exp && link == 0,
            Some(false) => exp = exp && link != 0,
            _ => {}
        }

        if let Some(r#in) = &self._in {
            exp = exp && r#in.iter().any(|item| *item as LinkType == link)
        }

        if let Some(nin) = &self.nin {
            exp = exp && nin.iter().all(|item| *item as LinkType != link)
        }

        exp
    }
}
