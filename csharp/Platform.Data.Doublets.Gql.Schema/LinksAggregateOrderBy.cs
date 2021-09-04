using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Data.Doublets.Gql.Schema.Types
{
    class LinksAggregateOrderBy
    {
        public LinksFieldsOrderBy _avg { get; set; }

        public order_by? count { get; set; }

        public LinksFieldsOrderBy max { get; set; }

        public LinksFieldsOrderBy min { get; set; }

        public LinksFieldsOrderBy stddev { get; set; }

        public LinksFieldsOrderBy stddev_pop { get; set; }

        public LinksFieldsOrderBy stddev_samp { get; set; }

        public LinksFieldsOrderBy sum { get; set; }

        public LinksFieldsOrderBy var_pop { get; set; }

        public LinksFieldsOrderBy var_samp { get; set; } 

        public LinksFieldsOrderBy variance { get; set; }

    }
}
