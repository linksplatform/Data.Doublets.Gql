using GraphQL.Types;

namespace GraphQL.Samples.Schemas.Link
{
    class LinkBooleanExpressionInputType : InputObjectGraphType
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
}/*        private ListGraphType<LinkBooleanExpressionInputType> _and;
        private ListGraphType<LinkBooleanExpressionInputType> _or;
        private LinkBooleanExpressionInputType _not;
        private LinkBooleanExpressionInputType from;
        private LongComparisonExpressionInputType from_id;
        private LongComparisonExpressionInputType id;
        private LinkBooleanExpressionInputType In;
        private LinkBooleanExpressionInputType Out;
        private LinkBooleanExpressionInputType to;
        private LongComparisonExpressionInputType to_id;
        private LinkBooleanExpressionInputType type;
        private LongComparisonExpressionInputType type_id;*/