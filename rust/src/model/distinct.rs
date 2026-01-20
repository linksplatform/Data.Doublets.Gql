use crate::model::LinkType;
use doublets::Link;
use std::hash::{Hash, Hasher};

pub struct DistinctWrapper {
    matches: (LinkType, LinkType),
    link: Link<LinkType>,
}

impl DistinctWrapper {
    pub fn from_match_link(matches: (LinkType, LinkType), link: Link<LinkType>) -> Self {
        Self { matches, link }
    }

    pub fn into_link(self) -> Link<LinkType> {
        self.link
    }
}

impl Hash for DistinctWrapper {
    fn hash<H: Hasher>(&self, state: &mut H) {
        self.matches.hash(state)
    }
}

impl PartialEq<Self> for DistinctWrapper {
    fn eq(&self, other: &Self) -> bool {
        &self.matches == &other.matches
    }
}

impl Eq for DistinctWrapper {}
