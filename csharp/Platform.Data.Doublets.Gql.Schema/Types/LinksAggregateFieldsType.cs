using GraphQL;
using GraphQL.Types;
using Platform.Data.Doublets.Gql.Schema.Types.Enums;

namespace Platform.Data.Doublets.Gql.Schema.Types
{
    using MappedType = LinksAggregateFields;

    /// <remarks>
    ///     """
    ///     aggregate fields of "links"
    ///     """
    ///     type links_aggregate_fields {
    ///     avg: links_avg_fields
    ///     count(columns: [links_select_column!], distinct: Boolean): Int
    ///     max: links_max_fields
    ///     min: links_min_fields
    ///     stddev: links_stddev_fields
    ///     stddev_pop: links_stddev_pop_fields
    ///     stddev_samp: links_stddev_samp_fields
    ///     sum: links_sum_fields
    ///     var_pop: links_var_pop_fields
    ///     var_samp: links_var_samp_fields
    ///     variance: links_variance_fields
    ///     }
    /// </remarks>
    public class LinksAggregateFieldsType : ObjectGraphType<MappedType>
    {
        public LinksAggregateFieldsType()
        {
            Name = "links_aggregate_fields";
            Field<LinksAggregateFloatAvgFieldsType>(nameof(MappedType.avg));
            Field<IntGraphType>(nameof(MappedType.count), null,
                new QueryArguments
                {
                    new QueryArgument<ListGraphType<NonNullGraphType<LinksSelectColumnEnumBaseType>>>
                        { Name = "columns" },
                    new QueryArgument<BooleanGraphType> { Name = "distinct" }
                }, ResolveCount);
            Field<LinksAggregateBigIntMaxFieldsType>(nameof(MappedType.max));
            Field<LinksAggregateBigIntMinFieldsType>(nameof(MappedType.min));
            Field<LinksAggregateFloatStddevFieldsType>(nameof(MappedType.stddev));
            Field<LinksAggregateFloatStdDevPopFieldsType>(nameof(MappedType.stddev_pop));
            Field<LinksAggregateFloatStdDevSampFieldsType>(nameof(MappedType.stddev_samp));
            Field<LinksAggregateBigIntSumFieldsType>(nameof(MappedType.sum));
            Field<LinksAggregateFloatVarPopFieldsType>(nameof(MappedType.var_pop));
            Field<LinksAggregateFloatVarSampFieldsType>(nameof(MappedType.var_samp));
            Field<LinksAggregateFloatVarianceFieldsType>(nameof(MappedType.variance));
        }

        private object ResolveCount(IResolveFieldContext<object> arg)
        {
            return 0;
        }
    }
}
