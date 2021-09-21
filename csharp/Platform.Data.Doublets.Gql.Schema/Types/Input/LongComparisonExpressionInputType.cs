using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    /// <remarks>
    ///     """
    ///     expression to compare columns of type bigint. All fields are combined with logical 'AND'.
    ///     """
    ///     input bigint_comparison_exp {
    ///     _eq: bigint
    ///     _gt: bigint
    ///     _gte: bigint
    ///     _in: [bigint!]
    ///     _is_null: Boolean
    ///     _lt: bigint
    ///     _lte: bigint
    ///     _neq: bigint
    ///     _nin: [bigint!]
    ///     }
    /// </remarks>
    public class LongComparisonExpressionInputType : InputObjectGraphType<LongComparisonExpression>
    {
        public LongComparisonExpressionInputType()
        {
            Name = "bigint_comparison_exp";
            Field(x => x._eq, true, typeof(LongGraphType));
            Field(x => x._gt, true, typeof(LongGraphType));
            Field(x => x._gte, true, typeof(LongGraphType));
            Field<ListGraphType<LongGraphType>>("_in");
            Field(x => x._is_null, true, typeof(BooleanGraphType));
            Field(x => x._lt, true, typeof(LongGraphType));
            Field(x => x._lte, true, typeof(LongGraphType));
            Field(x => x._neq, true, typeof(LongGraphType));
            Field<ListGraphType<LongGraphType>>("_nin");
        }
    }
}