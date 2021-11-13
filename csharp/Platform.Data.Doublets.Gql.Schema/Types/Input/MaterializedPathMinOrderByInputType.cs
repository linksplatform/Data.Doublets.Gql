using GraphQL.Types;
using Platform.Data.Doublets.Gql.Schema.Types.Enums;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    /// <remarks>
    ///     """
    ///     order by min() on columns of table "mp"
    ///     """
    ///     input mp_min_order_by {
    ///     group_id: order_by
    ///     id: order_by
    ///     insert_category: order_by
    ///     item_id: order_by
    ///     path_item_depth: order_by
    ///     path_item_id: order_by
    ///     position_id: order_by
    ///     root_id: order_by
    ///     }
    /// </remarks>
    public class MaterializedPathMinOrderByInputType : InputObjectGraphType<MaterializedPathMinOrderBy>
    {
        public MaterializedPathMinOrderByInputType()
        {
            Name = "mp_min_order_by";
            Description = "order by min() on columns of table \"mp\"";
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
