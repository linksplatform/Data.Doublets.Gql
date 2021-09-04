using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Data.Doublets.Gql.Schema.Types
{
    public class LinksAggregateBigIntFields
    {
        public long id { get; set; }
        public long from_id { get; set; }
        public long to_id { get; set; }
        public long type_id { get; set; }
    }
}
