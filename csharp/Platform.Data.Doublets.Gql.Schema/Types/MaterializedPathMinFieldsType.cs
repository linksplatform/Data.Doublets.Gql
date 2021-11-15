using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types
{
    public class MaterializedPathMinFieldsType : ObjectGraphType<MaterializedPathMinFields>
    {
        public MaterializedPathMinFieldsType()
        {
            Name = "mp_min_fields";
            Description = "aggregate min on columns";
            Field<LongGraphType>(nameof(MappedType.group_id));
            Field<LongGraphType>(nameof(MappedType.id));
            Field<string>(nameof(MappedType.insert_category));
            Field<LongGraphType>(nameof(MappedType.item_id));
            Field<LongGraphType>(nameof(MappedType.path_item_depth));
            Field<LongGraphType>(nameof(MappedType.path_item_id));
            Field<string>(nameof(MappedType.position_id));
            Field<LongGraphType>(nameof(MappedType.root_id));
        }
    }
}
