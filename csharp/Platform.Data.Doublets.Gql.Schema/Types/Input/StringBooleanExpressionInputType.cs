using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    /// <remarks>
    ///     """
    ///     Boolean expression to filter rows from the table "string". All fields are combined with a logical 'AND'.
    ///     """
    ///     input string_bool_exp {
    ///     _and: [string_bool_exp]
    ///     _not: string_bool_exp
    ///     _or: [string_bool_exp]
    ///     id: bigint_comparison_exp
    ///     link: links_bool_exp
    ///     link_id: bigint_comparison_exp
    ///     value: String_comparison_exp
    ///     }
    /// </remarks>
    public class StringBooleanExpressionInputType : InputObjectGraphType<StringBooleanExpression>
    {
        public StringBooleanExpressionInputType()
        {
            Name = "string_bool_exp";
            Field(x => x._and, true, typeof(ListGraphType<StringBooleanExpressionInputType>));
            Field(x => x._not, true, typeof(StringBooleanExpressionInputType));
            Field(x => x._or, true, typeof(ListGraphType<StringBooleanExpressionInputType>));
            Field(x => x.id, true, typeof(LongComparisonExpressionInputType));
            Field(x => x.link, true, typeof(LinksBooleanExpressionInputType));
            Field(x => x.link_id, true, typeof(LongComparisonExpressionInputType));
            Field(x => x.value, true, typeof(StringComparisonExpressionInputType));
        }
    }
}
