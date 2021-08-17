using Platform.Data.Doublets.Gql.Schema;
using GraphQL.Types;

namespace Input
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
            Field<ListGraphType<LongGraphType>>("_in");
            Field<ListGraphType<LongGraphType>>("_nin");
        }
    }
}