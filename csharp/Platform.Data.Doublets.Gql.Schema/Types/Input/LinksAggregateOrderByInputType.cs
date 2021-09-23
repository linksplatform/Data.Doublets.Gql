using GraphQL.Types;
using Platform.Data.Doublets.Gql.Schema.Types.Enum;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    /// <remarks>
    ///     """
    ///     order by aggregate values of table "links"
    ///     """
    ///     input links_aggregate_order_by {
    ///     avg: links_avg_order_by
    ///     count: order_by
    ///     max: links_max_order_by
    ///     min: links_min_order_by
    ///     stddev: links_stddev_order_by
    ///     stddev_pop: links_stddev_pop_order_by
    ///     stddev_samp: links_stddev_samp_order_by
    ///     sum: links_sum_order_by
    ///     var_pop: links_var_pop_order_by
    ///     var_samp: links_var_samp_order_by
    ///     variance: links_variance_order_by
    ///     }
    /// </remarks>
    public class LinksAggregateOrderByInputType : InputObjectGraphType<LinksAggregateOrderBy>
    {
        public LinksAggregateOrderByInputType()
        {
            Name = "links_aggregate_order_by";
            Field(x => x.avg, true, typeof(LinksFieldsAvgOrderByInputType));
            Field(x => x.count, true, typeof(OrderByEnumType));
            Field(x => x.max, true, typeof(LinksFieldsMaxOrderByInputType));
            Field(x => x.min, true, typeof(LinksFieldsMinOrderByInputType));
            Field(x => x.stddev, true, typeof(LinksFieldsStdDevOrderByInputType));
            Field(x => x.stddev_pop, true, typeof(LinksFieldsStdDevPopOrderByInputType));
            Field(x => x.stddev_samp, true, typeof(LinksFieldsStdDevSampOrderByInputType));
            Field(x => x.sum, true, typeof(LinksFieldsSumOrderByInputType));
            Field(x => x.var_pop, true, typeof(LinksFieldsVarPopOrderByInputType));
            Field(x => x.var_samp, true, typeof(LinksFieldsVarSampOrderByInputType));
            Field(x => x.variance, true, typeof(LinksFieldsVarianceOrderByInputType));
        }
    }
}
