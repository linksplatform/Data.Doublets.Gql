using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types
{
    public class MaterializedPathMinFieldsType : ObjectGraphType<MaterializedPathMinFields>
    {
        public MaterializedPathMinFieldsType()
        {
            Name = "mp_min_fields";
            Description = "aggregate min on columns";
            Field(x => x.group_id, true, typeof(LongGraphType));
            Field(x => x.id, true, typeof(LongGraphType));
            Field(x => x.insert_category, true, typeof(string));
            Field(x => x.item_id, true, typeof(LongGraphType));
            Field(x => x.path_item_depth, true, typeof(LongGraphType));
            Field(x => x.path_item_id, true, typeof(LongGraphType));
            Field(x => x.position_id, true, typeof(string));
            Field(x => x.root_id, true, typeof(LongGraphType));
        }
    }
}