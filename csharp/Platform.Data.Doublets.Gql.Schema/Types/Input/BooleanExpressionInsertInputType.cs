using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    public class BooleanExpressionInsertInputType : InputObjectGraphType<BooleanExpressionInsert>
    {
        public BooleanExpressionInsertInputType()
        {
            Name = "bool_exp_insert_input";
            Field<StringGraphType>(nameof(MappedType.gql));
            Field<LongGraphType>(nameof(MappedType.id));
            Field<LinksArrayRelationshipInsertInputType>(nameof(MappedType.link));
            Field<LongGraphType>(nameof(MappedType.link_id));
            Field<StringGraphType>(nameof(MappedType.sql));
        }
    }
}
