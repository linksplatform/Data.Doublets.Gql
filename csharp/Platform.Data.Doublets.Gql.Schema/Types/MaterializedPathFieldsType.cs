using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types
{
    public class MaterializedPathFieldsType : ObjectGraphType<MaterializedPathFields>
    {
        public MaterializedPathFieldsType()
        {
        }

        public MaterializedPathFieldsType(string name, string description = default)
        {
            Name = name;
            Description = description;
            Field<FloatGraphType>(nameof(MappedType.group_id));
            Field<FloatGraphType>(nameof(MappedType.id));
            Field<FloatGraphType>(nameof(MappedType.item_id));
            Field<FloatGraphType>(nameof(MappedType.path_item_depth));
            Field<FloatGraphType>(nameof(MappedType.path_item_id));
            Field<FloatGraphType>(nameof(MappedType.root_id));
        }
    }
}
