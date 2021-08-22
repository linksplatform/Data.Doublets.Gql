using GraphQL.Types;
using Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gql.Samples.Schemas.Link.Input
{
    class AggregateOrderByInputType : InputObjectGraphType
    {
        public AggregateOrderByInputType()
        {
            Field<AggregateOrderByFieldInputType>("avg_order_by");
            Field<OrderByInputType>("order_by");
            Field<AggregateOrderByFieldInputType>("max_order_by");
            Field<AggregateOrderByFieldInputType>("min_order_by");
            Field<AggregateOrderByFieldInputType>("stddev_order_by");
            Field<AggregateOrderByFieldInputType>("stddev_pop_order_by");
            Field<AggregateOrderByFieldInputType>("stddev_samp_order_by");
            Field<AggregateOrderByFieldInputType>("dc_dg_links_sum_order_by");
            Field<AggregateOrderByFieldInputType>("var_pop_order_by");
            Field<AggregateOrderByFieldInputType>("var_samp_order_by");
            Field<AggregateOrderByFieldInputType>("type");
            Field<AggregateOrderByFieldInputType>("type_id");
        }
    }
}
