using Gql.Samples.Schemas.Link.Types;
using GraphQL;
using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    internal class OrderByInputType : InputObjectGraphType<OrderBy>
    {
        public OrderByInputType()
        {
            Field<OrderByInputType>("from");
            Field(x => x.from_id, nullable: true, type: typeof(OrderByEnum));
            Field(x => x.id, nullable: true, type: typeof(OrderByEnum));
            Field<OrderByInputType>("to");
            Field(x => x.to_id, nullable: true, type: typeof(OrderByEnum));
            Field<OrderByInputType>("type");
            Field(x => x.type_id, nullable: true, type: typeof(OrderByEnum));
            Field<LinksAggregateOrderByInputType>("out_aggregate", null, LinkQuery.Arguments, ResolveOut, null);
            Field<LinksAggregateOrderByInputType>("in_aggreagate", null, LinkQuery.Arguments, ResolveIn, null);
        }
        private LinksAggregateOrderByInputType ResolveOut(IResolveFieldContext<OrderBy> context) => new LinksAggregateOrderByInputType();
        private LinksAggregateOrderByInputType ResolveIn(IResolveFieldContext<OrderBy> context) => new LinksAggregateOrderByInputType();
    }
}