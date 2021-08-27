using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    internal class LinkBooleanExpressionInputType : InputObjectGraphType
    {
        public LinkBooleanExpressionInputType()
        {
            Field<ListGraphType<LinkBooleanExpressionInputType>>("_and", null, true, null);
            Field<ListGraphType<LinkBooleanExpressionInputType>>("_or", null, true, null);
            Field<LinkBooleanExpressionInputType>("_not", null, true, null);
            Field<LinkBooleanExpressionInputType>("from", null, true, null);
            Field<LongComparisonExpressionInputType>("from_id", null, true, null);
            Field<LongComparisonExpressionInputType>("id", null, true, null);
            Field<LinkBooleanExpressionInputType>("in", null, true, null);
            Field<LinkBooleanExpressionInputType>("out", null, true, null);
            Field<LinkBooleanExpressionInputType>("to", null, true, null);
            Field<LongComparisonExpressionInputType>("to_id", null, true, null);
            Field<LinkBooleanExpressionInputType>("type", null, true, null);
            Field<LongComparisonExpressionInputType>("type_id", null, true, null);
        }
    }
}