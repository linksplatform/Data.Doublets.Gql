using GraphQL.Types;
using Platform.Data.Doublets.Gql.Schema.Types.Enums;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    public class MaterializedPathMaxOrderByInputType : InputObjectGraphType<MaterializedPathMaxOrderBy>
    {
        public MaterializedPathMaxOrderByInputType()
        {
            Name = "mp_max_order_by";
            Description = "order by max() on columns of table \"mp\"";
            Field(x => x.gpoup_id, true, typeof(OrderByEnumType));
            Field(x => x.id, true, typeof(OrderByEnumType));
            Field(x => x.insert_category, true, typeof(OrderByEnumType));
            Field(x => x.item_id, true, typeof(OrderByEnumType));
            Field(x => x.path_item_depth, true, typeof(OrderByEnumType));
            Field(x => x.path_item_id, true, typeof(OrderByEnumType));
            Field(x => x.path_item_id, true, typeof(OrderByEnumType));
            Field(x => x.position_id, true, typeof(OrderByEnumType));
            Field(x => x.root_id, true, typeof(OrderByEnumType));
        }
    }
}