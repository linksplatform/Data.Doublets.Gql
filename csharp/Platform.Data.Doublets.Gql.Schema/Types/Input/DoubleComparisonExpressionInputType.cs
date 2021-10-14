using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    /// <remarks>
    /// """
    /// expression to compare columns of type float8. All fields are combined with logical 'AND'.
    /// """
    /// input float8_comparison_exp {
    ///   _eq: float8
    ///   _gt: float8
    ///   _gte: float8
    ///   _in: [float8!]
    ///   _is_null: Boolean
    ///   _lt: float8
    ///   _lte: float8
    ///   _neq: float8
    ///   _nin: [float8!]
    /// }
    /// </remarks>
    public class DoubleComparisonExpressionInputType : InputObjectGraphType<DoubleComparisonExpression>
    {
        public DoubleComparisonExpressionInputType()
        {
            Name = "float8_comparison_exp";
            Field(x => x._eq, nullable: true, type: typeof(FloatGraphType));
            Field(x => x._gt, nullable: true, type: typeof(FloatGraphType));
            Field(x => x._gte, nullable: true, type: typeof(FloatGraphType));
            Field(x => x._in, nullable: true, type: typeof(ListGraphType<NonNullGraphType<FloatGraphType>>));
            Field(x => x._is_null, nullable: true, type: typeof(BooleanGraphType));
            Field(x => x._lt, nullable: true, type: typeof(FloatGraphType));
            Field(x => x._lte, nullable: true, type: typeof(FloatGraphType));
            Field(x => x._neq, nullable: true, type: typeof(FloatGraphType));
            Field(x => x._nin, nullable: true, type: typeof(ListGraphType<NonNullGraphType<FloatGraphType>>));
        }
    }
}
