using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    using MappedType = StringInsert;

    public class StringInsertInputType : InputObjectGraphType<MappedType>
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
