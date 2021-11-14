using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    public class NumberInsertInputType : InputObjectGraphType<NumberInsert>
    {
        public NumberInsertInputType()
        {
            Name = "number_insert_input";
            Field(x => x.id, true, typeof(LongGraphType));
            Field(x => x.link, true, typeof(LinksArrayRelationshipInsertInputType));
            Field(x => x.link_id, true, typeof(LongGraphType));
            Field(x => x.value, true, typeof(FloatGraphType));
        }
    }
}
