using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    public class LinksPkColumnsInputType : InputObjectGraphType
    {
        public LinksPkColumnsInputType()
        {
            Name = "links_pk_columns_input";
            Field<NonNullGraphType<LongGraphType>>("id");
        }
    }
}
