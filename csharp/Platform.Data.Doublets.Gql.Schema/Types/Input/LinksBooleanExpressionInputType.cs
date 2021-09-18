using GraphQL.Types;
using System;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    /// <remarks>
    /// """
    /// Boolean expression to filter rows from the table "links". All fields are combined with a logical 'AND'.
    /// """
    /// input links_bool_exp {
    ///    _and: [links_bool_exp]
    ///    _not: links_bool_exp
    ///    _or: [links_bool_exp]
    ///    from: links_bool_exp
    ///    from_id: bigint_comparison_exp
    ///    id: bigint_comparison_exp
    ///    in: links_bool_exp
    ///    out: links_bool_exp
    ///    to: links_bool_exp
    ///    to_id: bigint_comparison_exp
    ///    type: links_bool_exp
    ///    type_id: bigint_comparison_exp
    /// }
    /// </remarks>
    public class LinksBooleanExpressionInputType : InputObjectGraphType<LinksBooleanExpression>
    {
        public LinksBooleanExpressionInputType()
        {

            Field(x => x._and, nullable: true,type: typeof(ListGraphType<LinksBooleanExpressionInputType>));
            Field(x => x._not, nullable: true, type: typeof(LinksBooleanExpressionInputType));
            Field(x => x._or, nullable: true, type: typeof(ListGraphType<LinksBooleanExpressionInputType>));
            Field(x => x.from, nullable: true, type: typeof(LinksBooleanExpressionInputType));
            Field(x => x.from_id, nullable: true, type: typeof(LongComparisonExpressionInputType)); 
            Field(x => x.id, nullable: true, type: typeof(LongComparisonExpressionInputType));
            Field(x => x.@in, nullable: true, type: typeof(LinksBooleanExpressionInputType));
            Field(x => x.@out, nullable: true, type: typeof(LinksBooleanExpressionInputType));
            Field(x => x.to, nullable: true, type: typeof(LinksBooleanExpressionInputType));
            Field(x => x.to_id, nullable: true, type: typeof(LongComparisonExpressionInputType));
            Field(x => x.type, nullable: true, type: typeof(LinksBooleanExpressionInputType));
            Field(x => x.type_id, nullable: true, type: typeof(LongComparisonExpressionInputType));
        }
    }
}