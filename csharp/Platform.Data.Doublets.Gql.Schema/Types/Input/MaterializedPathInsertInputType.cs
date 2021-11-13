using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    using MappedType = MaterializedPathInsert;

    /// <remarks>
    ///     """
    ///     input type for inserting data into table "mp"
    ///     """
    ///     input mp_insert_input {
    ///     by_group: links_obj_rel_insert_input
    ///     by_item: mp_arr_rel_insert_input
    ///     by_path_item: mp_arr_rel_insert_input
    ///     by_position: mp_arr_rel_insert_input
    ///     by_root: mp_arr_rel_insert_input
    ///     group_id: bigint
    ///     id: bigint
    ///     insert_category: String
    ///     item: links_obj_rel_insert_input
    ///     item_id: bigint
    ///     path_item: links_obj_rel_insert_input
    ///     path_item_depth: bigint
    ///     path_item_id: bigint
    ///     position_id: String
    ///     root: links_obj_rel_insert_input
    ///     root_id: bigint
    ///     }
    /// </remarks>
    public class MaterializedPathInsertInputType : InputObjectGraphType<MappedType>
    {
        public MaterializedPathInsertInputType()
        {
            Name = "mp_insert_input";
            Description = "input type for inserting data into table \"mp\"";
            Field<LinksObjRelInsertInputType>(nameof(MappedType.by_group));
            Field<MaterializedPathArrayRelationshipInsertInputType>(nameof(MappedType.by_item));
            Field<MaterializedPathArrayRelationshipInsertInputType>(nameof(MappedType.by_path_item));
            Field<MaterializedPathArrayRelationshipInsertInputType>(nameof(MappedType.by_position));
            Field<MaterializedPathArrayRelationshipInsertInputType>(nameof(MappedType.by_root));
            Field<LongGraphType>(nameof(MappedType.group_id));
            Field<LongGraphType>(nameof(MappedType.id));
            Field<StringGraphType>(nameof(MappedType.insert_category));
            Field<LinksObjRelInsertInputType>(nameof(MappedType.item));
            Field<LongGraphType>(nameof(MappedType.item_id));
            Field<LinksObjRelInsertInputType>(nameof(MappedType.path_item));
            Field<LongGraphType>(nameof(MappedType.path_item_depth));
            Field<LongGraphType>(nameof(MappedType.path_item_id));
            Field<StringGraphType>(nameof(MappedType.position_id));
            Field<LinksObjRelInsertInputType>(nameof(MappedType.root));
            Field<LongGraphType>(nameof(MappedType.root_id));
        }
    }
}
