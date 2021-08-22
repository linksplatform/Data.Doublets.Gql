using GraphQL;
using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types
{
    class LinksAggregateFieldsType : ObjectGraphType
    {
        public LinksAggregateFieldsType()
        {
            Field<LinksAggregateFloatFieldsType>("avg");
            Field<IntGraphType>("count", null,
                new QueryArguments() {new QueryArgument<ListGraphType<LinksSelectColumnEnum>> { Name = "columns" }, 
                new QueryArgument<BooleanGraphType>{ Name = "distinct"}
                }, ResolveCount, null);
            Field<LinksAggregateBigIntFieldsType>("max");
            Field<LinksAggregateBigIntFieldsType>("min");
            Field<LinksAggregateFloatFieldsType>("stddev");
            Field<LinksAggregateFloatFieldsType>("stddev_pop");
            Field<LinksAggregateFloatFieldsType>("stddev_samp");
            Field<LongGraphType>("sum");
            Field<LinksAggregateFloatFieldsType>("var_pop");
            Field<LinksAggregateFloatFieldsType>("var_samp");
            Field<LinksAggregateFloatFieldsType>("variance");
        }

        private object ResolveCount(IResolveFieldContext<object> arg)
        {
            return 0;
        }
    }
}
