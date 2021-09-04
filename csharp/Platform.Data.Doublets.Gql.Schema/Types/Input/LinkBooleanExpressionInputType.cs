using GraphQL.Types;
using System;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    public class LinkBooleanExpressionInputType : InputObjectGraphType<LinkBooleanExpression>
    {
        public LinkBooleanExpressionInputType()
        {

            Field(x => x._and, nullable: true,type: typeof(ListGraphType<LinkBooleanExpressionInputType>));
            Field(x => x._or, nullable: true, type: typeof(ListGraphType<LinkBooleanExpressionInputType>));
            Field(x => x._not, nullable: true, type: typeof(LinkBooleanExpressionInputType));
            Field(x => x.from, nullable: true, type: typeof(LinkBooleanExpressionInputType));
            Field(x => x.from_id, nullable: true, type: typeof(LongComparisonExpressionInputType)); 
            Field(x => x.id, nullable: true, type: typeof(LongComparisonExpressionInputType));
            Field(x => x.@in, nullable: true, type: typeof(LinkBooleanExpressionInputType));
            Field(x => x.@out, nullable: true, type: typeof(LinkBooleanExpressionInputType));
            Field(x => x.to, nullable: true, type: typeof(LinkBooleanExpressionInputType));
            Field(x => x.to_id, nullable: true, type: typeof(LongComparisonExpressionInputType));
            Field(x => x.type, nullable: true, type: typeof(LinkBooleanExpressionInputType));
            Field(x => x.type_id, nullable: true, type: typeof(LongComparisonExpressionInputType));
        }
    }
}