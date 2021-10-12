using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    /// <remarks>
    ///     """
    ///     Boolean expression to filter rows from the table "links". All fields are combined with a logical 'AND'.
    ///     """
    ///     input links_bool_exp {
    ///     _and: [links_bool_exp]
    ///     _not: links_bool_exp
    ///     _or: [links_bool_exp]
    ///     from: links_bool_exp
    ///     from_id: bigint_comparison_exp
    ///     id: bigint_comparison_exp
    ///     in: links_bool_exp
    ///     out: links_bool_exp
    ///     to: links_bool_exp
    ///     to_id: bigint_comparison_exp
    ///     type: links_bool_exp
    ///     type_id: bigint_comparison_exp
    ///     }
    /// </remarks>
    public class LinksBooleanExpressionInputType : InputObjectGraphType<LinksBooleanExpression>
    {
        public LinksBooleanExpressionInputType()
        {
            Name = "links_bool_exp";
            Field(x => x._and, true, typeof(ListGraphType<LinksBooleanExpressionInputType>));
            Field(x => x._by_group, nullable: true, type: typeof(MaterializedPathBooleanExpressionInputType));
            Field(x => x._not, true, typeof(LinksBooleanExpressionInputType));
            Field(x => x._or, true, typeof(ListGraphType<LinksBooleanExpressionInputType>));
            Field(x => x.from, true, typeof(LinksBooleanExpressionInputType));
            Field(x => x.from_id, true, typeof(LongComparisonExpressionInputType));
            Field(x => x.id, true, typeof(LongComparisonExpressionInputType));
            Field(x => x.@in, true, typeof(LinksBooleanExpressionInputType));
            Field(x => x.@out, true, typeof(LinksBooleanExpressionInputType));
            Field(x => x.to, true, typeof(LinksBooleanExpressionInputType));
            Field(x => x.to_id, true, typeof(LongComparisonExpressionInputType));
            Field(x => x.type, true, typeof(LinksBooleanExpressionInputType));
            Field(x => x.type_id, true, typeof(LongComparisonExpressionInputType));
        }
    }
}
