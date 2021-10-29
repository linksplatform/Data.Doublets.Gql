using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    using MappedType = LinksInsert;

    /// <remarks>
    /// """
    /// input type for inserting data into table "links"
    /// """
    /// input links_insert_input {
    ///   _by_group: mp_arr_rel_insert_input
    ///   _by_item: mp_arr_rel_insert_input
    ///   _by_path_item: mp_arr_rel_insert_input
    ///   _by_root: mp_arr_rel_insert_input
    ///   bool_exp: bool_exp_obj_rel_insert_input
    ///   from: links_obj_rel_insert_input
    ///   from_id: bigint
    ///   id: bigint
    ///   in: links_arr_rel_insert_input
    ///   number: number_obj_rel_insert_input
    ///   out: links_arr_rel_insert_input
    ///   string: string_obj_rel_insert_input
    ///   to: links_obj_rel_insert_input
    ///   to_id: bigint
    ///   type: links_obj_rel_insert_input
    ///   type_id: bigint
    /// }
    /// </remarks>
    public class LinksInsertInputType : InputObjectGraphType<MappedType>
    {
        public LinksInsertInputType()
        {
            Name = "links_insert_input";
            Description = "input type for inserting data into table \"links\"";
            Field<MaterializedPathArrayRelationshipInsertInputType>(nameof(MappedType._by_group));
            Field<MaterializedPathArrayRelationshipInsertInputType>(nameof(MappedType._by_item));
            Field<MaterializedPathArrayRelationshipInsertInputType>(nameof(MappedType._by_path_item));
            Field<MaterializedPathArrayRelationshipInsertInputType>(nameof(MappedType._by_root));
            Field<BooleanExpressionObjectRelationshipInsertInputType>(nameof(MappedType.bool_exp));
            Field<LinksObjRelInsertInputType>(nameof(MappedType.from));
            Field<LongGraphType>(nameof(MappedType.from_id));
            Field<LongGraphType>(nameof(MappedType.id));
            Field<LinksArrayRelationshipInsertInputType>(nameof(MappedType.@in));
            Field<NumberObjectRelationshipInsertInputType>(nameof(MappedType.number));
            Field<LinksArrayRelationshipInsertInputType>(nameof(MappedType.@out));
            Field<StringObjectRelationshipInsertInputType>(nameof(MappedType.@string));
            Field<LinksObjRelInsertInputType>(nameof(MappedType.to));
            Field<LongGraphType>(nameof(MappedType.to_id));
            Field<LinksObjRelInsertInputType>(nameof(MappedType.type));
            Field<LongGraphType>(nameof(MappedType.type_id));
        }
    }
}
