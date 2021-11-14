using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    public class StringComparisonExpressionInputType : InputObjectGraphType<StringComparisonExpression>
    {
        public StringComparisonExpressionInputType()
        {
            Name = "String_comparison_exp";
            Field(x => x._eq, nullable: true, type: typeof(StringGraphType));
            Field(x => x._gt, nullable: true, type: typeof(StringGraphType));
            Field(x => x._gte, nullable: true, type: typeof(StringGraphType));
            Field(x => x._ilike, nullable: true, type: typeof(StringGraphType));
            Field(x => x._in, nullable: true, type: typeof(ListGraphType<NonNullGraphType<StringGraphType>>));
            Field(x => x._is_null, nullable: true, type: typeof(BooleanGraphType));
            Field(x => x._like, nullable: true, type: typeof(StringGraphType));
            Field(x => x._lt, nullable: true, type: typeof(StringGraphType));
            Field(x => x._lte, nullable: true, type: typeof(StringGraphType));
            Field(x => x._neq, nullable: true, type: typeof(StringGraphType));
            Field(x => x._nilike, nullable: true, type: typeof(StringGraphType));
            Field(x => x._nin, nullable: true, type: typeof(ListGraphType<NonNullGraphType<StringGraphType>>));
            Field(x => x._nlike, nullable: true, type: typeof(StringGraphType));
            Field(x => x._nsimilar, nullable: true, type: typeof(StringGraphType));
            Field(x => x._similar, nullable: true, type: typeof(StringGraphType));
        }
    }
}
