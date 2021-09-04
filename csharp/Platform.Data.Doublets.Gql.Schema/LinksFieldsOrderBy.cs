using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Data.Doublets.Gql.Schema.Types
{
    public class LinksFieldsOrderBy
    {
        public order_by? id { get; set; }

        public order_by? from_id { get; set; }

        public order_by? to_id { get; set; }

        public order_by? type_id { get; set; }
    }
}
