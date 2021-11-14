using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types
{
    public class LinksMutationResponseType : ObjectGraphType<LinksMutationResponse>
    {
        public LinksMutationResponseType()
        {
            Name = "links_mutation_response";
            Field<NonNullGraphType<IntGraphType>>("affected_rows");
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<LinksType>>>>("returning");
        }
    }
}