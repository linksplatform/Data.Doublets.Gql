using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    using MappedType = BooleanExpressionBooleanExpression;

    public class BooleanExpressionBooleanExpressionInputType : InputObjectGraphType<MappedType>
    {
        public BooleanExpressionBooleanExpressionInputType()
        {
            Name = "bool_exp_bool_exp";
            Field(x => x._and, true,
                typeof(ListGraphType<BooleanExpressionBooleanExpressionInputType>));
            Field(x => x._not, true,
                typeof(BooleanExpressionBooleanExpressionInputType));
            Field(x => x._or, true,
                typeof(ListGraphType<BooleanExpressionBooleanExpressionInputType>));
            Field(x => x.gql, true,
                typeof(StringComparisonExpressionInputType));
            Field(x => x.id, true,
                typeof(LongComparisonExpressionInputType));
            Field(x => x.link, true,
                typeof(LinksBooleanExpressionInputType));
            Field(x => x.link_id, true,
                typeof(LongComparisonExpressionInputType));
            Field(x => x.sql, true,
                typeof(StringComparisonExpressionInputType));
        }
    }
}
