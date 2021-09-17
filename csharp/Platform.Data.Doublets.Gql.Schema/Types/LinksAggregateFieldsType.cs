﻿using GraphQL;
using GraphQL.Types;
using Platform.Data.Doublets.Gql.Schema.Types.Input;

namespace Platform.Data.Doublets.Gql.Schema.Types
{
    /// <remarks>
    /// """
    /// aggregate fields of "links"
    /// """
    /// type links_aggregate_fields {
    ///   avg: links_avg_fields
    ///   count(columns: [links_select_column!], distinct: Boolean): Int
    ///   max: links_max_fields
    ///   min: links_min_fields
    ///   stddev: links_stddev_fields
    ///   stddev_pop: links_stddev_pop_fields
    ///   stddev_samp: links_stddev_samp_fields
    ///   sum: links_sum_fields
    ///   var_pop: links_var_pop_fields
    ///   var_samp: links_var_samp_fields
    ///   variance: links_variance_fields
    /// }
    /// </remarks>
    internal class LinksAggregateFieldsType : ObjectGraphType<LinksAggregateFields>
    {
        public LinksAggregateFieldsType()
        {
            Field(x => x.avg, nullable: true, type: typeof(LinksAggregateFloatFieldsType));
            Field<IntGraphType>("count", null,
                new QueryArguments() {new QueryArgument<ListGraphType<LinksColumnType>> { Name = "columns" },
                new QueryArgument<BooleanGraphType>{ Name = "distinct"}
                }, ResolveCount, null);
            Field(x => x.max, nullable: true, type: typeof(LinksAggregateBigIntFieldsType));
            Field(x => x.min, nullable: true, type: typeof(LinksAggregateBigIntFieldsType));
            Field(x => x.stddev, nullable: true, type: typeof(LinksAggregateFloatFieldsType));
            Field(x => x.stddev_pop, nullable: true, type: typeof(LinksAggregateFloatFieldsType));
            Field(x => x.stddev_samp, nullable: true, type: typeof(LinksAggregateFloatFieldsType));
            Field(x => x.sum, nullable: true, type: typeof(LinksAggregateBigIntFieldsType));
            Field(x => x.var_pop, nullable: true, type: typeof(LinksAggregateFloatFieldsType));
            Field(x => x.var_samp, nullable: true, type: typeof(LinksAggregateFloatFieldsType));
            Field(x => x.variance, nullable: true, type: typeof(LinksAggregateFloatFieldsType));
        }

        private object ResolveCount(IResolveFieldContext<object> arg) => 0 ;
    }
}
