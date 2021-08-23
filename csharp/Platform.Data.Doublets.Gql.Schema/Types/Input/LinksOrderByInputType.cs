using Gql.Samples.Schemas.Link.Types;
using GraphQL;
using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    internal class LinksOrderByInputType : InputObjectGraphType<OrderBy>
    {
        public LinksOrderByInputType()
        {
            Field<LinksOrderByInputType>("from");
            Field(x => x.from_id, nullable: true, type: typeof(OrderByEnumType));
            Field(x => x.id, nullable: true, type: typeof(OrderByEnumType));
            Field<LinksOrderByInputType>("to");
            Field(x => x.to_id, nullable: true, type: typeof(OrderByEnumType));
            Field<LinksOrderByInputType>("type");
            Field(x => x.type_id, nullable: true, type: typeof(OrderByEnumType));
            Field<LinksAggregateOrderByInputType>("out_aggregate");
            Field<LinksAggregateOrderByInputType>("in_aggreagate");
        }
    }
}