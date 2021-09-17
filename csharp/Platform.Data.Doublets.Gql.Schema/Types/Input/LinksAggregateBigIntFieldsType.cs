using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    /// <remarks>
    /// """
    /// order by max() on columns of table "links"
    /// """
    /// input links_max_order_by {
    ///   from_id: order_by
    ///   id: order_by
    ///   to_id: order_by
    ///   type_id: order_by
    /// }
    /// 
    /// """
    /// order by min() on columns of table "links"
    /// """
    /// input links_min_order_by {
    ///   from_id: order_by
    ///   id: order_by
    ///   to_id: order_by
    ///   type_id: order_by
    /// }
    /// """
    /// input type for updating data in table "links"
    /// """
    /// input links_set_input {
    ///   from_id: bigint
    ///   id: bigint
    ///   to_id: bigint
    ///   type_id: bigint
    /// }
    ///
    /// """
    /// order by sum() on columns of table "links"
    /// """
    /// input links_sum_order_by {
    /// from_id: order_by
    ///     id: order_by
    ///     to_id: order_by
    ///     type_id: order_by
    /// }
    /// </remarks>
    class LinksAggregateBigIntFieldsType : ObjectGraphType
    {
        public LinksAggregateBigIntFieldsType()
        {
            Field<LongGraphType>("id");
            Field<LongGraphType>("from_id");
            Field<LongGraphType>("to_id");
            Field<LongGraphType>("type_id");
        }
    }
}
