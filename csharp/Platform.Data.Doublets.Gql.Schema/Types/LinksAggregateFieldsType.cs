using GraphQL;
using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types
{
    internal class LinksAggregateFieldsType : ObjectGraphType
    {
        public LinksAggregateFieldsType()
        {
            Field<LinksAggregateFloatFieldsType>("avg", null, nullable: true, null);
            Field<IntGraphType>("count", null,
                new QueryArguments() {new QueryArgument<ListGraphType<LinksColumnType>> { Name = "columns" },
                new QueryArgument<BooleanGraphType>{ Name = "distinct"}
                }, ResolveCount, null);
            Field<LinksAggregateBigIntFieldsType>("max", null, nullable: true, null);
            Field<LinksAggregateBigIntFieldsType>("min", null, nullable: true, null);
            Field<LinksAggregateFloatFieldsType>("stddev", null, nullable: true, null);
            Field<LinksAggregateFloatFieldsType>("stddev_pop", null, nullable: true, null);
            Field<LinksAggregateFloatFieldsType>("stddev_samp", null, nullable: true, null);
            Field<LinksAggregateBigIntFieldsType>("sum", null, nullable: true, null);
            Field<LinksAggregateFloatFieldsType>("var_pop", null, nullable: true, null);
            Field<LinksAggregateFloatFieldsType>("var_samp", null, nullable: true, null);
            Field<LinksAggregateFloatFieldsType>("variance", null, nullable: true, null);
        }

        private object ResolveCount(IResolveFieldContext<object> arg) => 0;
    }
}
