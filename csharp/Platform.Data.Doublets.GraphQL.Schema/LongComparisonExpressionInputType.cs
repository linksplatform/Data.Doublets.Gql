using GraphQL.Types;

namespace GraphQL.Samples.Schemas.Link
{
    class LongComparisonExpressionInputType : InputObjectGraphType
    {
        public LongComparisonExpressionInputType()
        {
            Field<LongGraphType>("_eq");
            Field<LongGraphType>("_gt");
            Field<LongGraphType>("_gte");
            Field<ListGraphType<LongGraphType>>("_in");
            Field<BooleanGraphType>("is_null");
            Field<LongGraphType>("_lt");
            Field<LongGraphType>("_lte");
            Field<LongGraphType>("_neq");
            Field<ListGraphType<LongGraphType>>("_nin");
        }
    }
}
