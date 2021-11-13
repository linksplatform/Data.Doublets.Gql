using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types
{

    public class MaterializedPathMaxFieldsType : ObjectGraphType<MaterializedPathMaxFields>
    {
        public MaterializedPathMaxFieldsType()
        {
            Name = "mp_max_fields";
            Description = "aggregate max on columns";
            Field(x => x.group_id, true, typeof(FloatGraphType));
            Field(x => x.id, true, typeof(FloatGraphType));
            Field(x => x.item_id, true, typeof(FloatGraphType));
            Field(x => x.path_item_depth, true, typeof(FloatGraphType));
            Field(x => x.path_item_id, true, typeof(FloatGraphType));
            Field(x => x.root_id, true, typeof(FloatGraphType));
        }
    }
}
