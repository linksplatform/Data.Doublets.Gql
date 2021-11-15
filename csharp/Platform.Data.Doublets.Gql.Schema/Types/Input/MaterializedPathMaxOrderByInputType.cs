using GraphQL.Types;
using Platform.Data.Doublets.Gql.Schema.Types.Enums;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    using MappedType = MaterializedPathMaxOrderBy;
    public class MaterializedPathMaxOrderByInputType : InputObjectGraphType<MappedType>
    {
        public MaterializedPathMaxOrderByInputType()
        {
            Name = "mp_max_order_by";
            Description = "order by max() on columns of table \"mp\"";
            Field<OrderByEnumType>(nameof(MappedType.gpoup_id));
            Field<OrderByEnumType>(nameof(MappedType.id));
            Field<OrderByEnumType>(nameof(MappedType.insert_category));
            Field<OrderByEnumType>(nameof(MappedType.item_id));
            Field<OrderByEnumType>(nameof(MappedType.path_item_depth));
            Field<OrderByEnumType>(nameof(MappedType.path_item_id));
            Field<OrderByEnumType>(nameof(MappedType.path_item_id));
            Field<OrderByEnumType>(nameof(MappedType.position_id));
            Field<OrderByEnumType>(nameof(MappedType.root_id));
        }
    }
}
