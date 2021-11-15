using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    public class NumberInsertInputType : InputObjectGraphType<NumberInsert>
    {
        public NumberInsertInputType()
        {
            Name = "number_insert_input";
            Field<LongGraphType>(nameof(MappedType.id));
            Field<LinksArrayRelationshipInsertInputType>(nameof(MappedType.link));
            Field<LongGraphType>(nameof(MappedType.link_id));
            Field<FloatGraphType>(nameof(MappedType.value));
        }
    }
}
