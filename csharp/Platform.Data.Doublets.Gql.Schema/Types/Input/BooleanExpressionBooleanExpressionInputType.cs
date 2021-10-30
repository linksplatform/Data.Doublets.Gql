using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    /// <remarks>
    /// """
    /// Boolean expression to filter rows from the table "bool_exp". All fields are combined with a logical 'AND'.
    /// """
    /// input bool_exp_bool_exp {
    ///   _and: [bool_exp_bool_exp]
    ///   _not: bool_exp_bool_exp
    ///   _or: [bool_exp_bool_exp]
    ///   gql: String_comparison_exp
    ///   id: bigint_comparison_exp
    ///   link: links_bool_exp
    ///   link_id: bigint_comparison_exp
    ///   sql: String_comparison_exp
    /// }
    /// </remarks>
    public class BooleanExpressionBooleanExpressionInputType : InputObjectGraphType<BooleanExpressionBooleanExpression>
    {
        public BooleanExpressionBooleanExpressionInputType()
        {
            Name = "bool_exp_bool_exp";
            Field(x => x._and, nullable: true,
                type: typeof(ListGraphType<BooleanExpressionBooleanExpressionInputType>));
            Field(x => x._not, nullable: true,
                type: typeof(BooleanExpressionBooleanExpressionInputType));
            Field(x => x._or, nullable: true,
                type: typeof(ListGraphType<BooleanExpressionBooleanExpressionInputType>));
            Field(x => x.gql, nullable: true,
                type: typeof(StringComparisonExpressionInputType));
            Field(x => x.id, nullable: true,
                type: typeof(LongComparisonExpressionInputType));
            Field(x => x.link, nullable: true,
                type: typeof(LinksBooleanExpressionInputType));
            Field(x => x.link_id, nullable: true,
                type: typeof(LongComparisonExpressionInputType));
            Field(x => x.sql, nullable: true,
                type: typeof(StringComparisonExpressionInputType));

        }
    }
}
