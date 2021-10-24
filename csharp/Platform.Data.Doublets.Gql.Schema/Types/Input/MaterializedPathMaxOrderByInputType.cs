using GraphQL.Types;
using Platform.Data.Doublets.Gql.Schema.Types.Enum;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    /// <remarks>
    /// """
    /// order by max() on columns of table "mp"
    /// """
    /// input mp_max_order_by {
    ///   group_id: order_by
    ///   id: order_by
    ///   insert_category: order_by
    ///   item_id: order_by
    ///   path_item_depth: order_by
    ///   path_item_id: order_by
    ///   position_id: order_by
    ///   root_id: order_by
    /// }
    /// </remarks>
    public class MaterializedPathMaxOrderByInputType : ObjectGraphType<MaterializedPathMaxOrderBy>
    {
        public MaterializedPathMaxOrderByInputType()
        {
            Name = "mp_max_order_by";
            Description = "order by max() on columns of table \"mp\"";
            Field(x => x.gpoup_id, nullable: true, type: typeof(OrderByEnumType));
            Field(x => x.id, nullable: true, type: typeof(OrderByEnumType));
            Field(x => x.insert_category, nullable: true, type: typeof(OrderByEnumType));
            Field(x => x.item_id, nullable: true, type: typeof(OrderByEnumType));
            Field(x => x.path_item_depth, nullable: true, type: typeof(OrderByEnumType));
            Field(x => x.path_item_id, nullable: true, type: typeof(OrderByEnumType));
            Field(x => x.path_item_id, nullable: true, type: typeof(OrderByEnumType));
            Field(x => x.position_id, nullable: true, type: typeof(OrderByEnumType));
            Field(x => x.root_id, nullable: true, type: typeof(OrderByEnumType));
        }
    }
}
