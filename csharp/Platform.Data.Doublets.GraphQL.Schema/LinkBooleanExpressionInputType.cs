using GraphQL.Types;

namespace GraphQL.Samples.Schemas.Link
{
    class LinkBooleanExpressionInputType : InputObjectGraphType
    {
        public LinkBooleanExpressionInputType()
        {
            Field<ListGraphType<LinkBooleanExpressionInputType>>("_and");
            Field<ListGraphType<LinkBooleanExpressionInputType>>("_not");
            Field<LinkBooleanExpressionInputType>("_or");
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