using GraphQL;
using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types
{
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

        private object ResolveCount(IResolveFieldContext<object> arg) => 0;
    }
}
