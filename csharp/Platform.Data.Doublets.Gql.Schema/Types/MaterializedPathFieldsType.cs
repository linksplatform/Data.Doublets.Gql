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
            Field(x => x.group_id, nullable: true, type: typeof(FloatGraphType));
            Field(x => x.id, nullable: true, type: typeof(FloatGraphType));
            Field(x => x.item_id, nullable: true, type: typeof(FloatGraphType));
            Field(x => x.path_item_depth, nullable: true, type: typeof(FloatGraphType));
            Field(x => x.path_item_id, nullable: true, type: typeof(FloatGraphType));
            Field(x => x.root_id, nullable: true, type: typeof(FloatGraphType));
        }
    }
}
