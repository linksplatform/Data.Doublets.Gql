using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    public class BooleanExpressionInsertInputType : InputObjectGraphType<BooleanExpressionInsert>
    {
        public BooleanExpressionInsertInputType()
        {
            Name = "bool_exp_insert_input";
            Field(x => x.gql, true, typeof(StringGraphType));
            Field(x => x.id, true, typeof(LongGraphType));
            Field(x => x.link, true, typeof(LinksArrayRelationshipInsertInputType));
            Field(x => x.link_id, true, typeof(LongGraphType));
            Field(x => x.sql, true, typeof(StringGraphType));
        }
    }
}