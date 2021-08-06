using GraphQL.Types;

namespace GraphQL.Samples.Schemas.Link
{
    class LongComparisonExpressionInputType : InputObjectGraphType<LongComparisonExpression>
    {
        public LongComparisonExpressionInputType()
        {
            Field(x => x._eq, nullable: true, type: typeof(LongGraphType));
            Field(x => x._gte, nullable: true, type: typeof(LongGraphType));
            Field(x => x._gt, nullable: true, type: typeof(LongGraphType));
            Field(x => x.is_null, nullable: true, type: typeof(BooleanGraphType));
            Field(x => x._lt, nullable: true, type: typeof(LongGraphType));
            Field(x => x._lte, nullable: true, type: typeof(LongGraphType));
            Field(x => x._neq, nullable: true, type: typeof(LongGraphType));
            //Field<LongGraphType>("_eq");
            //Field<LongGraphType>("_gt");
            //Field<LongGraphType>("_gte");
            //Field<ListGraphType<LongGraphType>>("_in");
            //Field<BooleanGraphType>("is_null");
            //Field<LongGraphType>("_lt");
            //Field<LongGraphType>("_lte");
            //Field<LongGraphType>("_neq");
            //Field<ListGraphType<LongGraphType>>("_nin");
        }
    }
}