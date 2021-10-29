using GraphQL.Types;
using Platform.Data.Doublets.Gql.Schema.Types.Enum;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    using MappedType = LinksAggregateOrderBy;
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
    public class LinksAggregateOrderByInputType : InputObjectGraphType<MappedType>
    {
        public LinksAggregateOrderByInputType()
        {
            Name = "links_aggregate_order_by";
            Field<LinksFieldsAvgOrderByInputType>(nameof(MappedType.avg));
            Field<OrderByEnumType>(nameof(MappedType.count));
            Field<LinksFieldsMaxOrderByInputType>(nameof(MappedType.max));
            Field<LinksFieldsMinOrderByInputType>(nameof(MappedType.min));
            Field<LinksFieldsStdDevOrderByInputType>(nameof(MappedType.stddev));
            Field<LinksFieldsStdDevPopOrderByInputType>(nameof(MappedType.stddev_pop));
            Field<LinksFieldsStdDevSampOrderByInputType>(nameof(MappedType.stddev_samp));
            Field<LinksFieldsSumOrderByInputType>(nameof(MappedType.sum));
            Field<LinksFieldsVarPopOrderByInputType>(nameof(MappedType.var_pop));
            Field<LinksFieldsVarSampOrderByInputType>(nameof(MappedType.var_samp));
            Field<LinksFieldsVarianceOrderByInputType>(nameof(MappedType.variance));
        }
    }
}
