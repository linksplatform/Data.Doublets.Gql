using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    using MappedType = LongComparisonExpression;

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
    public class LongComparisonExpressionInputType : InputObjectGraphType<MappedType>
    {
        public LongComparisonExpressionInputType()
        {
            Name = "bigint_comparison_exp";
            Field<LongGraphType>(nameof(MappedType._eq));
            Field<LongGraphType>(nameof(MappedType._gt));
            Field<LongGraphType>(nameof(MappedType._gte));
            Field<ListGraphType<NonNullGraphType<LongGraphType>>>(nameof(MappedType._in));
            Field<BooleanGraphType>(nameof(MappedType._is_null));
            Field<LongGraphType>(nameof(MappedType._lt));
            Field<LongGraphType>(nameof(MappedType._lte));
            Field<LongGraphType>(nameof(MappedType._neq));
            Field<ListGraphType<NonNullGraphType<LongGraphType>>>(nameof(MappedType._nin));
        }
    }
}