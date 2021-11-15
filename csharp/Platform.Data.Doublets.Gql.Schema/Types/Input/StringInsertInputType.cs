using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    public class StringInsertInputType : InputObjectGraphType<StringInsert>
    {
        public StringInsertInputType()
        {
            Name = "string_insert_input";
            Field<LongGraphType>(nameof(MappedType.id));
            Field<LinksArrayRelationshipInsertInputType>(nameof(MappedType.link));
            Field<LongGraphType>(nameof(MappedType.link_id));
            Field<StringGraphType>(nameof(MappedType.value));
        }
    }
}
