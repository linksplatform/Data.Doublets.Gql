using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    public class StringComparisonExpressionInputType : InputObjectGraphType<StringComparisonExpression>
    {
        public StringComparisonExpressionInputType()
        {
            Name = "String_comparison_exp";
            Field(x => x._eq, true, typeof(StringGraphType));
            Field(x => x._gt, true, typeof(StringGraphType));
            Field(x => x._gte, true, typeof(StringGraphType));
            Field(x => x._ilike, true, typeof(StringGraphType));
            Field(x => x._in, true, typeof(ListGraphType<NonNullGraphType<StringGraphType>>));
            Field(x => x._is_null, true, typeof(BooleanGraphType));
            Field(x => x._like, true, typeof(StringGraphType));
            Field(x => x._lt, true, typeof(StringGraphType));
            Field(x => x._lte, true, typeof(StringGraphType));
            Field(x => x._neq, true, typeof(StringGraphType));
            Field(x => x._nilike, true, typeof(StringGraphType));
            Field(x => x._nin, true, typeof(ListGraphType<NonNullGraphType<StringGraphType>>));
            Field(x => x._nlike, true, typeof(StringGraphType));
            Field(x => x._nsimilar, true, typeof(StringGraphType));
            Field(x => x._similar, true, typeof(StringGraphType));
        }
    }
}