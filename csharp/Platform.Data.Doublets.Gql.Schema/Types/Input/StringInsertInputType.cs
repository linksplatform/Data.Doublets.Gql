using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    public class StringInsertInputType : InputObjectGraphType<StringInsert>
    {
        public StringInsertInputType()
        {
            Name = "string_insert_input";
            Field(x => x.id, true, typeof(LongGraphType));
            Field(x => x.link, true, typeof(LinksArrayRelationshipInsertInputType));
            Field(x => x.link_id, true, typeof(LongGraphType));
            Field(x => x.value, true, typeof(StringGraphType));
        }
    }
}
