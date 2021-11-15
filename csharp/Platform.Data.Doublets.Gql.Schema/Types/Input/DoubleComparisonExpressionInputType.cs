using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    using MappedType = DoubleComparisonExpression;

    public class DoubleComparisonExpressionInputType : InputObjectGraphType<MappedType>
    {
        public DoubleComparisonExpressionInputType()
        {
            Name = "float8_comparison_exp";
            Field<FloatGraphType>(nameof(MappedType._eq));
            Field<FloatGraphType>(nameof(MappedType._gt));
            Field<FloatGraphType>(nameof(MappedType._gte));
            Field<ListGraphType<NonNullGraphType<FloatGraphType>>>(nameof(MappedType._in));
            Field<BooleanGraphType>(nameof(MappedType._is_null));
            Field<FloatGraphType>(nameof(MappedType._lt));
            Field<FloatGraphType>(nameof(MappedType._lte));
            Field<FloatGraphType>(nameof(MappedType._neq));
            Field<ListGraphType<NonNullGraphType<FloatGraphType>>>(nameof(MappedType._nin));
        }
    }
}
