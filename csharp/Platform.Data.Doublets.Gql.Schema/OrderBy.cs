using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Data.Doublets.Gql.Schema
{
    public enum order_by
    {
        asc,
        asc_nulls_first,
        asc_nulls_last,
        desc,
        desc_nulls_first,
        desc_nulls_lasr
    }

    class OrderBy
    {

        public OrderBy from { get; set; }

        public order_by? from_id { get; set; }

        public order_by? id { get; set; }

        public OrderBy to { get; set; }

        public order_by? to_id { get; set; }

        public OrderBy type { get; set; }

        public order_by? type_id { get; set; }
    }
    /*
     * input dc_dg_links_order_by {
  from: dc_dg_links_order_by
  from_id: order_by
  id: order_by
  in_aggregate: dc_dg_links_aggregate_order_by
  out_aggregate: dc_dg_links_aggregate_order_by
  to: dc_dg_links_order_by
  to_id: order_by
  type: dc_dg_links_order_by
  type_id: order_by
}
     */
}
