using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    /// <remarks>
    /// """
    /// expression to compare columns of type bigint. All fields are combined with logical 'AND'.
    /// """
    /// input bigint_comparison_exp {
    ///   _eq: bigint
    ///   _gt: bigint
    ///   _gte: bigint
    ///   _in: [bigint!]
    ///   _is_null: Boolean
    ///   _lt: bigint
    ///   _lte: bigint
    ///   _neq: bigint
    ///   _nin: [bigint!]
    /// }
    /// </remarks>
    internal class LongComparisonExpressionInputType : InputObjectGraphType<LongComparisonExpression>
    {
        public LongComparisonExpressionInputType()
        {
            Field(x => x._eq, nullable: true, type: typeof(LongGraphType));
            Field(x => x._gte, nullable:true, type: typeof(LongGraphType));
            Field(x => x._gt, nullable:  true, type: typeof(LongGraphType));
            Field(x => x._is_null, nullable: true, type: typeof(BooleanGraphType));
            Field(x => x._lt, nullable:  true, type: typeof(LongGraphType));
            Field(x => x._lte, nullable: true, type: typeof(LongGraphType));
            Field(x => x._neq, nullable: true, type: typeof(LongGraphType));
            Field<ListGraphType<LongGraphType>>("_in");
            Field<ListGraphType<LongGraphType>>("_nin");
        }
    }
}