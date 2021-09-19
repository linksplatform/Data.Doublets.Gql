using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types
{
    /// <remarks>
    /// """
    /// response of any mutation on the table "links"
    /// """
    /// type links_mutation_response {
    ///   """number of affected rows by the mutation"""
    ///   affected_rows: Int!
    ///
    ///   """data of the affected rows by the mutation"""
    ///   returning: [links!]!
    /// }
    /// </remarks>
    internal class LinksMutationResponseType : ObjectGraphType
    {
        public LinksMutationResponseType()
        {
            Name = "links_mutation_response";
            Field<IntGraphType>("affected_rows");
            Field<ListGraphType<LinksType>>("returning");
        }
    }
}
