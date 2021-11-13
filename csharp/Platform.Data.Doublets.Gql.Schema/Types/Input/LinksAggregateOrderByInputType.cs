using GraphQL.Types;
using Platform.Data.Doublets.Gql.Schema.Types.Enums;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    using MappedType = LinksAggregateOrderBy;
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
