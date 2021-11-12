using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    /// <remarks>
    /// """
    /// Boolean expression to filter rows from the table "number". All fields are combined with a logical 'AND'.
    /// """
    /// input number_bool_exp {
    ///   _and: [number_bool_exp]
    ///   _not: number_bool_exp
    ///   _or: [number_bool_exp]
    ///   id: bigint_comparison_exp
    ///   link: links_bool_exp
    ///   link_id: bigint_comparison_exp
    ///   value: float8_comparison_exp
    /// } 
    /// </remarks>
    public class NumberBooleanExpressionInputType : InputObjectGraphType<NumberBooleanExpression>
    {
        public NumberBooleanExpressionInputType()
        {
            Name = "number_bool_exp";
            Field(x => x._and, nullable: true, type: typeof(ListGraphType<NumberBooleanExpressionInputType>));
            Field(x => x._not, nullable: true, type: typeof(NumberBooleanExpressionInputType));
            Field(x => x._or, nullable: true, type: typeof(ListGraphType<NumberBooleanExpressionInputType>));
            Field(x => x.id, nullable: true, type: typeof(LongComparisonExpressionInputType));
            Field(x => x.link, nullable: true, type: typeof(LinksBooleanExpressionInputType));
            Field(x => x.link_id, nullable: true, type: typeof(LongComparisonExpressionInputType));
            Field(x => x.value, nullable: true, type: typeof(DoubleComparisonExpressionInputType));
        }
    }
}