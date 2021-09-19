using System.Collections.Generic;

namespace Platform.Data.Doublets.Gql.Schema
{
    public class LinksMutationResponse
    {
        public int affected_rows { get; set; }

        public List<Link> returning { get; set; }
    }
}
