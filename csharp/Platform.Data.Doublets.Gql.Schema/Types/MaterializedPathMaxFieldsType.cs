using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types
{
    public class MaterializedPathMaxFieldsType : ObjectGraphType<MaterializedPathMaxFields>
    {
        public MaterializedPathMaxFieldsType()
        {
            Name = "mp_max_fields";
            Description = "aggregate max on columns";
            Field<FloatGraphType>(nameof(MappedType.group_id));
            Field<FloatGraphType>(nameof(MappedType.id));
            Field<FloatGraphType>(nameof(MappedType.item_id));
            Field<FloatGraphType>(nameof(MappedType.path_item_depth));
            Field<FloatGraphType>(nameof(MappedType.path_item_id));
            Field<FloatGraphType>(nameof(MappedType.root_id));
        }
    }
}
