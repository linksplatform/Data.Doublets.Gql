using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    internal class LinkBooleanExpressionInputType : InputObjectGraphType
    {
        public LinkBooleanExpressionInputType()
        {
            Field<ListGraphType<LinkBooleanExpressionInputType>>("_and", null, nullable: true, null);
            Field<ListGraphType<LinkBooleanExpressionInputType>>("_or", null, nullable: true, null);
            Field<LinkBooleanExpressionInputType>("_not", null, nullable: true, null);
            Field<LinkBooleanExpressionInputType>("from", null, nullable: true, null);
            Field<LongComparisonExpressionInputType>("from_id", null, nullable: true, null);
            Field<LongComparisonExpressionInputType>("id", null, nullable: true, null);
            Field<LinkBooleanExpressionInputType>("in", null, true, null);
            Field<LinkBooleanExpressionInputType>("out", null, true, null);
            Field<LinkBooleanExpressionInputType>("to", null, nullable: true, null);
            Field<LongComparisonExpressionInputType>("to_id", null, nullable: true, null);
            Field<LinkBooleanExpressionInputType>("type", null, nullable: true, null);
            Field<LongComparisonExpressionInputType>("type_id", null, nullable: true, null);
        }
    }
}