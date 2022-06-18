use crate::model::Bigint;
use crate::model::Links;
use crate::model::LinksBoolExp;
use crate::model::LinksIncInput;
use crate::model::LinksInsertInput;
use crate::model::LinksMutationResponse;
use crate::model::LinksOnConflict;
use crate::model::LinksPkColumnsInput;
use crate::model::LinksSetInput;
use crate::model::Mp;
use crate::model::MpBoolExp;
use crate::model::MpIncInput;
use crate::model::MpInsertInput;
use crate::model::MpMutationResponse;
use crate::model::MpOnConflict;
use crate::model::MpPkColumnsInput;
use crate::model::MpSetInput;
use crate::model::Numbers;
use crate::model::NumbersBoolExp;
use crate::model::NumbersIncInput;
use crate::model::NumbersInsertInput;
use crate::model::NumbersMutationResponse;
use crate::model::NumbersOnConflict;
use crate::model::NumbersPkColumnsInput;
use crate::model::NumbersSetInput;
use crate::model::Objects;
use crate::model::ObjectsAppendInput;
use crate::model::ObjectsBoolExp;
use crate::model::ObjectsDeleteAtPathInput;
use crate::model::ObjectsDeleteElemInput;
use crate::model::ObjectsDeleteKeyInput;
use crate::model::ObjectsIncInput;
use crate::model::ObjectsInsertInput;
use crate::model::ObjectsMutationResponse;
use crate::model::ObjectsOnConflict;
use crate::model::ObjectsPkColumnsInput;
use crate::model::ObjectsPrependInput;
use crate::model::ObjectsSetInput;
use crate::model::Strings;
use crate::model::StringsBoolExp;
use crate::model::StringsIncInput;
use crate::model::StringsInsertInput;
use crate::model::StringsMutationResponse;
use crate::model::StringsOnConflict;
use crate::model::StringsPkColumnsInput;
use crate::model::StringsSetInput;
use async_graphql::*;
#[derive(Debug)]
pub struct MutationRoot;
#[Object(name = "mutation_root")]
impl MutationRoot {
    #[graphql(name = "delete_links")]
    pub async fn delete_links(
        &self,
        ctx: &Context<'_>,
        _where: LinksBoolExp,
    ) -> Option<LinksMutationResponse> {
        todo!()
    }
    #[graphql(name = "delete_links_by_pk")]
    pub async fn delete_links_by_pk(&self, ctx: &Context<'_>, id: Bigint) -> Option<Links> {
        todo!()
    }
    #[graphql(name = "delete_mp")]
    pub async fn delete_mp(
        &self,
        ctx: &Context<'_>,
        _where: MpBoolExp,
    ) -> Option<MpMutationResponse> {
        todo!()
    }
    #[graphql(name = "delete_mp_by_pk")]
    pub async fn delete_mp_by_pk(&self, ctx: &Context<'_>, id: Bigint) -> Option<Mp> {
        todo!()
    }
    #[graphql(name = "delete_numbers")]
    pub async fn delete_numbers(
        &self,
        ctx: &Context<'_>,
        _where: NumbersBoolExp,
    ) -> Option<NumbersMutationResponse> {
        todo!()
    }
    #[graphql(name = "delete_numbers_by_pk")]
    pub async fn delete_numbers_by_pk(&self, ctx: &Context<'_>, id: Bigint) -> Option<Numbers> {
        todo!()
    }
    #[graphql(name = "delete_objects")]
    pub async fn delete_objects(
        &self,
        ctx: &Context<'_>,
        _where: ObjectsBoolExp,
    ) -> Option<ObjectsMutationResponse> {
        todo!()
    }
    #[graphql(name = "delete_objects_by_pk")]
    pub async fn delete_objects_by_pk(&self, ctx: &Context<'_>, id: Bigint) -> Option<Objects> {
        todo!()
    }
    #[graphql(name = "delete_strings")]
    pub async fn delete_strings(
        &self,
        ctx: &Context<'_>,
        _where: StringsBoolExp,
    ) -> Option<StringsMutationResponse> {
        todo!()
    }
    #[graphql(name = "delete_strings_by_pk")]
    pub async fn delete_strings_by_pk(&self, ctx: &Context<'_>, id: Bigint) -> Option<Strings> {
        todo!()
    }
    #[graphql(name = "insert_links")]
    pub async fn insert_links(
        &self,
        ctx: &Context<'_>,
        objects: Vec<LinksInsertInput>,
        #[graphql(name = "on_conflict")] on_conflict: Option<LinksOnConflict>,
    ) -> Option<LinksMutationResponse> {
        todo!()
    }
    #[graphql(name = "insert_links_one")]
    pub async fn insert_links_one(
        &self,
        ctx: &Context<'_>,
        object: LinksInsertInput,
        #[graphql(name = "on_conflict")] on_conflict: Option<LinksOnConflict>,
    ) -> Option<Links> {
        todo!()
    }
    #[graphql(name = "insert_mp")]
    pub async fn insert_mp(
        &self,
        ctx: &Context<'_>,
        objects: Vec<MpInsertInput>,
        #[graphql(name = "on_conflict")] on_conflict: Option<MpOnConflict>,
    ) -> Option<MpMutationResponse> {
        todo!()
    }
    #[graphql(name = "insert_mp_one")]
    pub async fn insert_mp_one(
        &self,
        ctx: &Context<'_>,
        object: MpInsertInput,
        #[graphql(name = "on_conflict")] on_conflict: Option<MpOnConflict>,
    ) -> Option<Mp> {
        todo!()
    }
    #[graphql(name = "insert_numbers")]
    pub async fn insert_numbers(
        &self,
        ctx: &Context<'_>,
        objects: Vec<NumbersInsertInput>,
        #[graphql(name = "on_conflict")] on_conflict: Option<NumbersOnConflict>,
    ) -> Option<NumbersMutationResponse> {
        todo!()
    }
    #[graphql(name = "insert_numbers_one")]
    pub async fn insert_numbers_one(
        &self,
        ctx: &Context<'_>,
        object: NumbersInsertInput,
        #[graphql(name = "on_conflict")] on_conflict: Option<NumbersOnConflict>,
    ) -> Option<Numbers> {
        todo!()
    }
    #[graphql(name = "insert_objects")]
    pub async fn insert_objects(
        &self,
        ctx: &Context<'_>,
        objects: Vec<ObjectsInsertInput>,
        #[graphql(name = "on_conflict")] on_conflict: Option<ObjectsOnConflict>,
    ) -> Option<ObjectsMutationResponse> {
        todo!()
    }
    #[graphql(name = "insert_objects_one")]
    pub async fn insert_objects_one(
        &self,
        ctx: &Context<'_>,
        object: ObjectsInsertInput,
        #[graphql(name = "on_conflict")] on_conflict: Option<ObjectsOnConflict>,
    ) -> Option<Objects> {
        todo!()
    }
    #[graphql(name = "insert_strings")]
    pub async fn insert_strings(
        &self,
        ctx: &Context<'_>,
        objects: Vec<StringsInsertInput>,
        #[graphql(name = "on_conflict")] on_conflict: Option<StringsOnConflict>,
    ) -> Option<StringsMutationResponse> {
        todo!()
    }
    #[graphql(name = "insert_strings_one")]
    pub async fn insert_strings_one(
        &self,
        ctx: &Context<'_>,
        object: StringsInsertInput,
        #[graphql(name = "on_conflict")] on_conflict: Option<StringsOnConflict>,
    ) -> Option<Strings> {
        todo!()
    }
    #[graphql(name = "update_links")]
    pub async fn update_links(
        &self,
        ctx: &Context<'_>,
        #[graphql(name = "_inc")] inc: Option<LinksIncInput>,
        #[graphql(name = "_set")] set: Option<LinksSetInput>,
        _where: LinksBoolExp,
    ) -> Option<LinksMutationResponse> {
        todo!()
    }
    #[graphql(name = "update_links_by_pk")]
    pub async fn update_links_by_pk(
        &self,
        ctx: &Context<'_>,
        #[graphql(name = "_inc")] inc: Option<LinksIncInput>,
        #[graphql(name = "_set")] set: Option<LinksSetInput>,
        #[graphql(name = "pk_columns")] pk_columns: LinksPkColumnsInput,
    ) -> Option<Links> {
        todo!()
    }
    #[graphql(name = "update_mp")]
    pub async fn update_mp(
        &self,
        ctx: &Context<'_>,
        #[graphql(name = "_inc")] inc: Option<MpIncInput>,
        #[graphql(name = "_set")] set: Option<MpSetInput>,
        _where: MpBoolExp,
    ) -> Option<MpMutationResponse> {
        todo!()
    }
    #[graphql(name = "update_mp_by_pk")]
    pub async fn update_mp_by_pk(
        &self,
        ctx: &Context<'_>,
        #[graphql(name = "_inc")] inc: Option<MpIncInput>,
        #[graphql(name = "_set")] set: Option<MpSetInput>,
        #[graphql(name = "pk_columns")] pk_columns: MpPkColumnsInput,
    ) -> Option<Mp> {
        todo!()
    }
    #[graphql(name = "update_numbers")]
    pub async fn update_numbers(
        &self,
        ctx: &Context<'_>,
        #[graphql(name = "_inc")] inc: Option<NumbersIncInput>,
        #[graphql(name = "_set")] set: Option<NumbersSetInput>,
        _where: NumbersBoolExp,
    ) -> Option<NumbersMutationResponse> {
        todo!()
    }
    #[graphql(name = "update_numbers_by_pk")]
    pub async fn update_numbers_by_pk(
        &self,
        ctx: &Context<'_>,
        #[graphql(name = "_inc")] inc: Option<NumbersIncInput>,
        #[graphql(name = "_set")] set: Option<NumbersSetInput>,
        #[graphql(name = "pk_columns")] pk_columns: NumbersPkColumnsInput,
    ) -> Option<Numbers> {
        todo!()
    }
    #[graphql(name = "update_objects")]
    pub async fn update_objects(
        &self,
        ctx: &Context<'_>,
        #[graphql(name = "_append")] append: Option<ObjectsAppendInput>,
        #[graphql(name = "_delete_at_path")] delete_at_path: Option<ObjectsDeleteAtPathInput>,
        #[graphql(name = "_delete_elem")] delete_elem: Option<ObjectsDeleteElemInput>,
        #[graphql(name = "_delete_key")] delete_key: Option<ObjectsDeleteKeyInput>,
        #[graphql(name = "_inc")] inc: Option<ObjectsIncInput>,
        #[graphql(name = "_prepend")] prepend: Option<ObjectsPrependInput>,
        #[graphql(name = "_set")] set: Option<ObjectsSetInput>,
        _where: ObjectsBoolExp,
    ) -> Option<ObjectsMutationResponse> {
        todo!()
    }
    #[graphql(name = "update_objects_by_pk")]
    pub async fn update_objects_by_pk(
        &self,
        ctx: &Context<'_>,
        #[graphql(name = "_append")] append: Option<ObjectsAppendInput>,
        #[graphql(name = "_delete_at_path")] delete_at_path: Option<ObjectsDeleteAtPathInput>,
        #[graphql(name = "_delete_elem")] delete_elem: Option<ObjectsDeleteElemInput>,
        #[graphql(name = "_delete_key")] delete_key: Option<ObjectsDeleteKeyInput>,
        #[graphql(name = "_inc")] inc: Option<ObjectsIncInput>,
        #[graphql(name = "_prepend")] prepend: Option<ObjectsPrependInput>,
        #[graphql(name = "_set")] set: Option<ObjectsSetInput>,
        #[graphql(name = "pk_columns")] pk_columns: ObjectsPkColumnsInput,
    ) -> Option<Objects> {
        todo!()
    }
    #[graphql(name = "update_strings")]
    pub async fn update_strings(
        &self,
        ctx: &Context<'_>,
        #[graphql(name = "_inc")] inc: Option<StringsIncInput>,
        #[graphql(name = "_set")] set: Option<StringsSetInput>,
        _where: StringsBoolExp,
    ) -> Option<StringsMutationResponse> {
        todo!()
    }
    #[graphql(name = "update_strings_by_pk")]
    pub async fn update_strings_by_pk(
        &self,
        ctx: &Context<'_>,
        #[graphql(name = "_inc")] inc: Option<StringsIncInput>,
        #[graphql(name = "_set")] set: Option<StringsSetInput>,
        #[graphql(name = "pk_columns")] pk_columns: StringsPkColumnsInput,
    ) -> Option<Strings> {
        todo!()
    }
}