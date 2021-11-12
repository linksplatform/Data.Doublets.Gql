using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types
{
    /// <remarks>
    ///     """
    ///     response of any mutation on the table "links"
    ///     """
    ///     type links_mutation_response {
    ///     """number of affected rows by the mutation"""
    ///     affected_rows: Int!
    ///     """data of the affected rows by the mutation"""
    ///     returning: [links!]!
    ///     }
    /// </remarks>
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