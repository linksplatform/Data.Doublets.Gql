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
            Field<LocomparisonExpressionInputType>("from_id");
            Field<LocomparisonExpressionInputType>("id");
            Field<LinkBooleanExpressionInputType>("in");
            Field<LinkBooleanExpressionInputType>("out");
            Field<LinkBooleanExpressionInputType>("to");
            Field<LocomparisonExpressionInputType>("to_id");
            Field<LinkBooleanExpressionInputType>("type");
            Field<LocomparisonExpressionInputType>("type_id");
        }
    }
}