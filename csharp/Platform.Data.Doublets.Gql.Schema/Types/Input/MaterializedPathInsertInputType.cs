using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    using MappedType = MaterializedPathInsert;

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
