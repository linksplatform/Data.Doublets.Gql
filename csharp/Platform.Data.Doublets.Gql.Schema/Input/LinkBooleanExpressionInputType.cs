using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Input
{
    internal class LinkBooleanExpressionInputType : InputObjectGraphType
    {
        public LinkBooleanExpressionInputType()
        {
            Field<ListGraphType<LinkBooleanExpressionInputType>>("_and");
            Field<ListGraphType<LinkBooleanExpressionInputType>>("_or");
            Field<LinkBooleanExpressionInputType>("_not");
            Field<LinkBooleanExpressionInputType>("from");
            Field<LongComparisonExpressionInputType>("from_id");
            Field<LongComparisonExpressionInputType>("id");
            Field<LinkBooleanExpressionInputType>("in");
            Field<LinkBooleanExpressionInputType>("out");
            Field<LinkBooleanExpressionInputType>("to");
            Field<LongComparisonExpressionInputType>("to_id");
            Field<LinkBooleanExpressionInputType>("type");
            Field<LongComparisonExpressionInputType>("type_id");
        }
    }
}