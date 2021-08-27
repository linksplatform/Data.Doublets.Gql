using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    internal class LinksAggregateOrderByInputType : InputObjectGraphType
    {
        public LinksAggregateOrderByInputType()
        {
            Field<LinksFieldsOrderByInputType>("avg", null, nullable: true, null);
            Field<OrderByEnumType>("count", null, nullable: true, null);
            Field<LinksFieldsOrderByInputType>("max", null, nullable: true, null);
            Field<LinksFieldsOrderByInputType>("min", null, nullable: true, null);
            Field<LinksFieldsOrderByInputType>("stddev", null, nullable: true, null);
            Field<LinksFieldsOrderByInputType>("stddev_pop", null, nullable: true, null);
            Field<LinksFieldsOrderByInputType>("stddev_samp", null, nullable: true, null);
            Field<LinksFieldsOrderByInputType>("sum", null, nullable: true, null);
            Field<LinksFieldsOrderByInputType>("var_pop", null, nullable: true, null);
            Field<LinksFieldsOrderByInputType>("var_samp", null, nullable: true, null);
            Field<LinksFieldsOrderByInputType>("variance", null, nullable: true, null);
        }
    }
}
