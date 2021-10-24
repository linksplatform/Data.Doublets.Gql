using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types
{
    /// <remarks>
    /// """aggregate max on columns"""
    /// type mp_max_fields {
    ///   group_id: bigint
    ///   id: bigint
    ///   insert_category: String
    ///   item_id: bigint
    ///   path_item_depth: bigint
    ///   path_item_id: bigint
    ///   position_id: String
    ///   root_id: bigint
    /// }
    /// </remarks>
    public class MaterializedPathMaxFieldsType : ObjectGraphType<MaterializedPathMaxFields>
    {
        public MaterializedPathMaxFieldsType()
        {
            Name = "mp_max_fields";
            Description = "aggregate max on columns";
            Field(x => x.group_id, nullable: true, type: typeof(FloatGraphType));
            Field(x => x.id, nullable: true, type: typeof(FloatGraphType));
            Field(x => x.item_id, nullable: true, type: typeof(FloatGraphType));
            Field(x => x.path_item_depth, nullable: true, type: typeof(FloatGraphType));
            Field(x => x.path_item_id, nullable: true, type: typeof(FloatGraphType));
            Field(x => x.root_id, nullable: true, type: typeof(FloatGraphType));
        }
    }
}
