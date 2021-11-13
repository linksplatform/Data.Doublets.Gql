using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{

    public class DoubleComparisonExpressionInputType : InputObjectGraphType<DoubleComparisonExpression>
    {
        public DoubleComparisonExpressionInputType()
        {
            Name = "float8_comparison_exp";
            Field(x => x._eq, true, typeof(FloatGraphType));
            Field(x => x._gt, true, typeof(FloatGraphType));
            Field(x => x._gte, true, typeof(FloatGraphType));
            Field(x => x._in, true, typeof(ListGraphType<NonNullGraphType<FloatGraphType>>));
            Field(x => x._is_null, true, typeof(BooleanGraphType));
            Field(x => x._lt, true, typeof(FloatGraphType));
            Field(x => x._lte, true, typeof(FloatGraphType));
            Field(x => x._neq, true, typeof(FloatGraphType));
            Field(x => x._nin, true, typeof(ListGraphType<NonNullGraphType<FloatGraphType>>));
        }
    }
}
