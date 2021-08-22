using GraphQL;
using GraphQL.Types;
using Platform.Data.Doublets.Gql.Schema;
using System.Collections.Generic;
using System.Linq;

namespace Platform.Data.Doublets.Gql.Schema.Input
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
            Field<AggregateOrderByInputType>("out_aggregate", null, LinkQuery.Arguments, ResolveOut, null);
            Field<AggregateOrderByInputType>("in_aggreagate", null, LinkQuery.Arguments, ResolveIn, null);
        }
        private AggregateOrderByInputType ResolveOut(IResolveFieldContext<OrderBy> context)
        {
            return new AggregateOrderByInputType();
        }
        private AggregateOrderByInputType ResolveIn(IResolveFieldContext<OrderBy> context)
        {
            return new AggregateOrderByInputType();
        }
    }
}