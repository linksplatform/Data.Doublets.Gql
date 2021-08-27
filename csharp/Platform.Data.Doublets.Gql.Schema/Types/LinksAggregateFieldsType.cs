using GraphQL;
using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types
{
    internal class LinksAggregateFieldsType : ObjectGraphType
    {
        public LinksAggregateFieldsType()
        {
            Field<LinksAggregateFloatFieldsType>("avg", null, true, null);
            Field<IntGraphType>("count", null,
                new QueryArguments() {new QueryArgument<ListGraphType<LinksColumnType>> { Name = "columns" },
                new QueryArgument<BooleanGraphType>{ Name = "distinct"}
                }, ResolveCount, null);
            Field<LinksAggregateBigIntFieldsType>("max", null, true, null);
            Field<LinksAggregateBigIntFieldsType>("min", null, true, null);
            Field<LinksAggregateFloatFieldsType>("stddev", null, true, null);
            Field<LinksAggregateFloatFieldsType>("stddev_pop", null, true, null);
            Field<LinksAggregateFloatFieldsType>("stddev_samp", null, true, null);
            Field<LinksAggregateBigIntFieldsType>("sum", null, true, null);
            Field<LinksAggregateFloatFieldsType>("var_pop", null, true, null);
            Field<LinksAggregateFloatFieldsType>("var_samp", null, true, null);
            Field<LinksAggregateFloatFieldsType>("variance", null, true, null);
        }

        private object ResolveCount(IResolveFieldContext<object> arg) => 0;
    }
}
