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
            Field(x => x.from, nullable: true, type: typeof(LinksOrderByInputType));
            Field(x => x.from_id, nullable: true, type: typeof(OrderByEnumType));
            Field(x => x.id, nullable: true, type: typeof(OrderByEnumType));
            Field(x => x.in_aggregate, nullable: true, type: typeof(LinksAggregateOrderByInputType));
            Field(x => x.out_aggregate, nullable: true, type: typeof(LinksAggregateOrderByInputType));
            Field(x => x.to, nullable: true, type: typeof(LinksOrderByInputType));
            Field(x => x.to_id, nullable: true, type: typeof(OrderByEnumType));
            Field(x => x.type, nullable: true, type: typeof(LinksOrderByInputType));
            Field(x => x.type_id, nullable: true, type: typeof(OrderByEnumType));
        }
    }
}
