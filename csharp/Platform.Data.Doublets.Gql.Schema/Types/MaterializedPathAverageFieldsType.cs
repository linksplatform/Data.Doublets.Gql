using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types
{
    using MappedType = MaterializedPathAverageFields;

    public class MaterializedPathAverageFieldsType : ObjectGraphType<MappedType>
    {
        public MaterializedPathAverageFieldsType()
        {
            Name = "mp_avg_fields";
            Description = "aggregate avg on columns";
            Field<FloatGraphType>(nameof(MappedType.group_id));
            Field<FloatGraphType>(nameof(MappedType.id));
            Field<FloatGraphType>(nameof(MappedType.item_id));
            Field<FloatGraphType>(nameof(MappedType.path_item_depth));
            Field<FloatGraphType>(nameof(MappedType.path_item_id));
            Field<FloatGraphType>(nameof(MappedType.root_id));
        }
    }
}
