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
            Field(x => x.group_id, true, typeof(FloatGraphType));
            Field(x => x.id, true, typeof(FloatGraphType));
            Field(x => x.item_id, true, typeof(FloatGraphType));
            Field(x => x.path_item_depth, true, typeof(FloatGraphType));
            Field(x => x.path_item_id, true, typeof(FloatGraphType));
            Field(x => x.root_id, true, typeof(FloatGraphType));
        }
    }
}