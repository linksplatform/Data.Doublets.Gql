using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    internal class LinksAggregateOrderByInputType : InputObjectGraphType<LinksAggregateOrderBy>
    {
        public LinksAggregateOrderByInputType()
        {
            Field(x => x._avg, nullable: true, type: typeof(LinksAggregateOrderByInputType));
            Field(x => x.max, nullable: true, type: typeof(LinksAggregateOrderByInputType));
            Field(x => x.min, nullable: true, type: typeof(LinksAggregateOrderByInputType));
            Field(x => x.stddev, nullable: true, type: typeof(LinksAggregateOrderByInputType));
            Field(x => x.stddev_pop, nullable: true, type: typeof(LinksAggregateOrderByInputType));
            Field(x => x.stddev_samp, nullable: true, type: typeof(LinksAggregateOrderByInputType));
            Field(x => x.sum, nullable: true, type: typeof(LinksAggregateOrderByInputType));
            Field(x => x.var_pop, nullable: true, type: typeof(LinksAggregateOrderByInputType));
            Field(x => x.var_samp, nullable: true, type: typeof(LinksAggregateOrderByInputType));
            Field(x => x.variance, nullable: true, type: typeof(LinksAggregateOrderByInputType));
        }
    }
}
