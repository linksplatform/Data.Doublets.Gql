using GraphQL.Types;
using Platform.Data.Doublets.Gql.Schema.Types.Enums;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    using MappedType = LinksOrderBy;

    public class LinksOrderByInputType : InputObjectGraphType<MappedType>
    {
        public LinksOrderByInputType()
        {
            Name = "links_order_by";
            Field<LinksOrderByInputType>(nameof(MappedType.from));
            Field<OrderByEnumType>(nameof(MappedType.from_id));
            Field<OrderByEnumType>(nameof(MappedType.id));
            Field<LinksAggregateOrderByInputType>(nameof(MappedType.in_aggregate));
            Field<LinksAggregateOrderByInputType>(nameof(MappedType.out_aggregate));
            Field<LinksOrderByInputType>(nameof(MappedType.to));
            Field<OrderByEnumType>(nameof(MappedType.to_id));
            Field<LinksOrderByInputType>(nameof(MappedType.type));
            Field<OrderByEnumType>(nameof(MappedType.type_id));
        }
    }
}
