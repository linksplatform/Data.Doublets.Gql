using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    public class NumberBooleanExpressionInputType : InputObjectGraphType<NumberBooleanExpression>
    {
        public NumberBooleanExpressionInputType()
        {
            Name = "number_bool_exp";
            Field(x => x._and, true, typeof(ListGraphType<NumberBooleanExpressionInputType>));
            Field(x => x._not, true, typeof(NumberBooleanExpressionInputType));
            Field(x => x._or, true, typeof(ListGraphType<NumberBooleanExpressionInputType>));
            Field(x => x.id, true, typeof(LongComparisonExpressionInputType));
            Field(x => x.link, true, typeof(LinksBooleanExpressionInputType));
            Field(x => x.link_id, true, typeof(LongComparisonExpressionInputType));
            Field(x => x.value, true, typeof(DoubleComparisonExpressionInputType));
        }
    }
}