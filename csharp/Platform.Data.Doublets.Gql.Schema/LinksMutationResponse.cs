using System.Collections.Generic;

namespace Platform.Data.Doublets.Gql.Schema
{

    public class LinksMutationResponse
    {
        public int affected_rows;

        public List<Links> returning;
    }
}
