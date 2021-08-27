using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    internal class LinksAggregateOrderByInputType : InputObjectGraphType
    {
        public LinksAggregateOrderByInputType()
        {
            Field<LinksFieldsOrderByInputType>("avg", null, true, null);
            Field<OrderByEnumType>("count", null, true, null);
            Field<LinksFieldsOrderByInputType>("max", null, true, null);
            Field<LinksFieldsOrderByInputType>("min", null, true, null);
            Field<LinksFieldsOrderByInputType>("stddev", null, true, null);
            Field<LinksFieldsOrderByInputType>("stddev_pop", null, true, null);
            Field<LinksFieldsOrderByInputType>("stddev_samp", null, true, null);
            Field<LinksFieldsOrderByInputType>("sum", null, true, null);
            Field<LinksFieldsOrderByInputType>("var_pop", null, true, null);
            Field<LinksFieldsOrderByInputType>("var_samp", null, true, null);
            Field<LinksFieldsOrderByInputType>("variance", null, true, null);
        }
    }
}
