use crate::model::iterator::AtLeastAnyExt;
use crate::model::CanBoolExp;
use crate::model::JsonbComparisonExp;
use crate::model::MpBoolExp;
use crate::model::NumbersBoolExp;
use crate::model::ObjectsBoolExp;
use crate::model::SelectorsBoolExp;
use crate::model::StringsBoolExp;
use crate::model::{BigintComparisonExp, LinkType};
use crate::{store, RawStore};
use async_graphql::*;
use doublets::{Doublets, Link};

#[derive(InputObject, Debug)]
#[graphql(name = "links_bool_exp")]
pub struct LinksBoolExp {
    #[graphql(name = "_and")]
    pub and: Option<Vec<LinksBoolExp>>,
    #[graphql(name = "_by_group")]
    pub by_group: Option<Box<MpBoolExp>>,
    #[graphql(name = "_by_item")]
    pub by_item: Option<Box<MpBoolExp>>,
    #[graphql(name = "_by_path_item")]
    pub by_path_item: Option<Box<MpBoolExp>>,
    #[graphql(name = "_by_root")]
    pub by_root: Option<Box<MpBoolExp>>,
    #[graphql(name = "_not")]
    pub not: Option<Box<LinksBoolExp>>,
    #[graphql(name = "_or")]
    pub or: Option<Vec<LinksBoolExp>>,
    #[graphql(name = "can_action")]
    pub can_action: Option<Box<CanBoolExp>>,
    #[graphql(name = "can_object")]
    pub can_object: Option<Box<CanBoolExp>>,
    #[graphql(name = "can_rule")]
    pub can_rule: Option<Box<CanBoolExp>>,
    #[graphql(name = "can_subject")]
    pub can_subject: Option<Box<CanBoolExp>>,
    pub from: Option<Box<LinksBoolExp>>,
    #[graphql(name = "from_id")]
    pub from_id: Option<Box<BigintComparisonExp>>,
    pub id: Option<Box<BigintComparisonExp>>,
    pub _in: Option<Box<LinksBoolExp>>,
    pub number: Option<Box<NumbersBoolExp>>,
    pub object: Option<Box<ObjectsBoolExp>>,
    pub out: Option<Box<LinksBoolExp>>,
    pub selected: Option<Box<SelectorsBoolExp>>,
    pub selectors: Option<Box<SelectorsBoolExp>>,
    pub string: Option<Box<StringsBoolExp>>,
    pub to: Option<Box<LinksBoolExp>>,
    #[graphql(name = "to_id")]
    pub to_id: Option<Box<BigintComparisonExp>>,
    pub _type: Option<Box<LinksBoolExp>>,
    #[graphql(name = "type_id")]
    pub type_id: Option<Box<BigintComparisonExp>>,
    pub typed: Option<Box<LinksBoolExp>>,
    pub value: Option<Box<JsonbComparisonExp>>,
}

impl LinksBoolExp {
    pub fn matches(&self, store: &store::Store, link: &Link<LinkType>) -> bool {
        let mut exp = true;

        if let Some(id) = &self.id {
            exp = exp && id.matches(link.index);
        }

        if let Some(from_id) = &self.from_id {
            exp = exp && from_id.matches(link.source);
        }

        if let Some(to_id) = &self.to_id {
            exp = exp && to_id.matches(link.target);
        }

        let any = store.constants().any;

        if let Some(r#in) = &self._in {
            exp = exp
                && store
                    .each_iter([any, any, link.index])
                    .least_all(|link| r#in.matches(store, &link));
        }

        if let Some(out) = &self.out {
            exp = exp
                && store
                    .each_iter([any, link.index, any])
                    .least_all(|link| out.matches(store, &link));
        }

        if let Some(from) = &self.from {
            exp = exp
                && if let Some(link) = store.get_link(link.source) {
                    from.matches(store, &link)
                } else {
                    false
                };
        }

        if let Some(to) = &self.to {
            exp = exp
                && if let Some(link) = store.get_link(link.source) {
                    to.matches(store, &link)
                } else {
                    false
                };
        }

        if let Some(and) = &self.and {
            exp = exp && and.iter().all(|exp| exp.matches(store, link));
        }

        if let Some(or) = &self.or {
            exp = exp || or.iter().any(|exp| exp.matches(store, link));
        }

        if let Some(not) = &self.not {
            exp = exp && !not.matches(store, link);
        }

        exp
    }
}
