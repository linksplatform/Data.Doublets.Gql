using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Data.Doublets.Gql.Schema
{
    class LinksMutationResponse
    {
        public int affected_rows { get; set; }

        public List<IList<ulong>> returning { get; set; }
    }
}
