using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{

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
