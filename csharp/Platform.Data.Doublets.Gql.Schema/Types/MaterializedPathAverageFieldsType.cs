using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types
{
    /// <remarks>
    ///     """aggregate avg on columns"""
    ///     type mp_avg_fields {
    ///     group_id: Float
    ///     id: Float
    ///     item_id: Float
    ///     path_item_depth: Float
    ///     path_item_id: Float
    ///     root_id: Float
    ///     }
    /// </remarks>
    public class MaterializedPathAverageFieldsType : ObjectGraphType<MaterializedPathAverageFields>
    {
        public MaterializedPathAverageFieldsType()
        {
            Name = "mp_avg_fields";
            Description = "aggregate avg on columns";
            Field(x => x.group_id, true, typeof(FloatGraphType));
            Field(x => x.id, true, typeof(FloatGraphType));
            Field(x => x.item_id, true, typeof(FloatGraphType));
            Field(x => x.path_item_depth, true, typeof(FloatGraphType));
            Field(x => x.path_item_id, true, typeof(FloatGraphType));
            Field(x => x.root_id, true, typeof(FloatGraphType));
        }
    }
}
