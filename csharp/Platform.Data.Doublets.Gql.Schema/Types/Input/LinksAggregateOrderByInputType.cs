using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    /// <remarks>
    /// """
    /// order by aggregate values of table "links"
    /// """
    /// input links_aggregate_order_by {
    ///   avg: links_avg_order_by
    ///   count: order_by
    ///   max: links_max_order_by
    ///   min: links_min_order_by
    ///   stddev: links_stddev_order_by
    ///   stddev_pop: links_stddev_pop_order_by
    ///   stddev_samp: links_stddev_samp_order_by
    ///   sum: links_sum_order_by
    ///   var_pop: links_var_pop_order_by
    ///   var_samp: links_var_samp_order_by
    ///   variance: links_variance_order_by
    /// }
    /// </remarks>
    internal class LinksAggregateOrderByInputType : InputObjectGraphType<LinksAggregateOrderBy>
    {
        public LinksAggregateOrderByInputType()
        {
            Field(x => x.avg, nullable: true, type: typeof(LinksAggregateOrderByInputType));
            Field(x => x.count, nullable: true, type: typeof(OrderByEnumType));
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
