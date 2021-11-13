using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    using MappedType = LongComparisonExpression;
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
