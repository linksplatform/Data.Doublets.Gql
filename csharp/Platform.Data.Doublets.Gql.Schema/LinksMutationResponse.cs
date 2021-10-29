using System.Collections.Generic;

namespace Platform.Data.Doublets.Gql.Schema
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
    public class LinksMutationResponse
    {
        public int affected_rows { get; set; }

        public List<Links> returning { get; set; }
    }
}
