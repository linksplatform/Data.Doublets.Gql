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
            Field<LinksAggregateType>("out_aggregate", null, LinkQuery.Arguments, ResolveOut, null);
            Field<LinksAggregateType>("in_aggreagate", null, LinkQuery.Arguments, ResolveIn, null);
        }
        private LinksAggregateType ResolveOut(IResolveFieldContext<OrderBy> context) => new LinksAggregateType();
        private LinksAggregateType ResolveIn(IResolveFieldContext<OrderBy> context) => new LinksAggregateType();
    }
}