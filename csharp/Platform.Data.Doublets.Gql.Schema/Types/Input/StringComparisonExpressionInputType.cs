using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    public class StringComparisonExpressionInputType : InputObjectGraphType<StringComparisonExpression>
    {
        public StringComparisonExpressionInputType()
        {
            Name = "String_comparison_exp";
            Field<StringGraphType>(nameof(MappedType._eq));
            Field<StringGraphType>(nameof(MappedType._gt));
            Field<StringGraphType>(nameof(MappedType._gte));
            Field<StringGraphType>(nameof(MappedType._ilike));
            Field<ListGraphType<NonNullGraphType<StringGraphType>>>(nameof(MappedType._in));
            Field<BooleanGraphType>(nameof(MappedType._is_null));
            Field<StringGraphType>(nameof(MappedType._like));
            Field<StringGraphType>(nameof(MappedType._lt));
            Field<StringGraphType>(nameof(MappedType._lte));
            Field<StringGraphType>(nameof(MappedType._neq));
            Field<StringGraphType>(nameof(MappedType._nilike));
            Field<ListGraphType<NonNullGraphType<StringGraphType>>>(nameof(MappedType._nin));
            Field<StringGraphType>(nameof(MappedType._nlike));
            Field<StringGraphType>(nameof(MappedType._nsimilar));
            Field<StringGraphType>(nameof(MappedType._similar));
        }
    }
}
