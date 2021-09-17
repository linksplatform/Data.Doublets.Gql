using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    /// <remarks>
    // """
    // order by avg() on columns of table "links"
    // """
    // input links_avg_order_by {
    //   from_id: order_by
    //   id: order_by
    //   to_id: order_by
    //   type_id: order_by
    // }
    // 
    // """
    // order by stddev() on columns of table "links"
    // """
    // input links_stddev_order_by {
    // from_id: order_by
    //     id: order_by
    //     to_id: order_by
    //     type_id: order_by
    // }
    //
    // """
    // order by stddev_pop() on columns of table "links"
    // """
    // input links_stddev_pop_order_by {
    // from_id: order_by
    //     id: order_by
    //     to_id: order_by
    //     type_id: order_by
    // }
    //
    // """
    // order by stddev_samp() on columns of table "links"
    // """
    // input links_stddev_samp_order_by {
    // from_id: order_by
    //     id: order_by
    //     to_id: order_by
    //     type_id: order_by
    // }
    //
    // """
    // order by var_pop() on columns of table "links"
    // """
    // input links_var_pop_order_by {
    // from_id: order_by
    //     id: order_by
    //     to_id: order_by
    //     type_id: order_by
    // }
    //
    // """
    // order by var_samp() on columns of table "links"
    // """
    // input links_var_samp_order_by {
    // from_id: order_by
    //     id: order_by
    //     to_id: order_by
    //     type_id: order_by
    // }
    //
    // """
    // order by variance() on columns of table "links"
    // """
    // input links_variance_order_by {
    // from_id: order_by
    //     id: order_by
    //     to_id: order_by
    //     type_id: order_by
    // }
    /// </remarks>
    class LinksAggregateFloatFieldsInputType : InputObjectGraphType
    {
        public LinksAggregateFloatFieldsInputType()
        {
            Field<FloatGraphType>("id");
            Field<FloatGraphType>("from_id");
            Field<FloatGraphType>("to_id");
            Field<FloatGraphType>("type_id");
        }
    }
}
