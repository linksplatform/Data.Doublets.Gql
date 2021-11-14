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
            Field(x => x.from, true, typeof(LinksOrderByInputType));
            Field(x => x.from_id, true, typeof(OrderByEnumType));
            Field(x => x.id, true, typeof(OrderByEnumType));
            Field(x => x.in_aggregate, true, typeof(LinksAggregateOrderByInputType));
            Field(x => x.out_aggregate, true, typeof(LinksAggregateOrderByInputType));
            Field(x => x.to, true, typeof(LinksOrderByInputType));
            Field(x => x.to_id, true, typeof(OrderByEnumType));
            Field(x => x.type, true, typeof(LinksOrderByInputType));
            Field(x => x.type_id, true, typeof(OrderByEnumType));
        }
    }
}
