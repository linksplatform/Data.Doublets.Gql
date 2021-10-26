/* using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    public class MaterializedPathAggregateOrderByInputType : ObjectGraphType<MaterializedPathAggregateOrderBy>
    {
        Name = "links_aggregate_fields";
        Field(x => x.avg, true, typeof(LinksAggregateFloatAvgFieldsType));
        Field<IntGraphType>(nameof(MappedType.count), null,
        new QueryArguments
        {
            new QueryArgument<ListGraphType<NonNullGraphType<LinksSelectColumnEnumType>>> { Name = "columns" },
            new QueryArgument<BooleanGraphType> { Name = "distinct" }
        }, ResolveCount);
        Field(x => x.max, true, typeof(LinksAggregateBigIntMaxFieldsType));
        Field(x => x.min, true, typeof(LinksAggregateBigIntMinFieldsType));
        Field(x => x.stddev, true, typeof(LinksAggregateFloatStddevFieldsType));
        Field(x => x.stddev_pop, true, typeof(LinksAggregateFloatStdDevPopFieldsType));
        Field(x => x.stddev_samp, true, typeof(LinksAggregateFloatStdDevSampFieldsType));
        Field(x => x.sum, true, typeof(LinksAggregateBigIntSumFieldsType));
        Field(x => x.var_pop, true, typeof(LinksAggregateFloatVarPopFieldsType));
        Field(x => x.var_samp, true, typeof(LinksAggregateFloatVarSampFieldsType));
        Field(x => x.variance, true, typeof(LinksAggregateFloatVarianceFieldsType));
    }
}
*/
