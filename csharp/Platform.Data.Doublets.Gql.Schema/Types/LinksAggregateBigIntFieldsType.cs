using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    /// <remarks>
    /// """aggregate max on columns"""
    /// type links_max_fields {
    ///  from_id: bigint
    ///  id: bigint
    ///  to_id: bigint
    ///  type_id: bigint
    /// }
    ///
    /// """aggregate min on columns"""
    /// type links_min_fields {
    ///  from_id: bigint
    ///  id: bigint
    ///  to_id: bigint
    ///  type_id: bigint
    /// }
    ///
    /// """aggregate sum on columns"""
    /// type links_sum_fields {
    ///  from_id: bigint
    ///  id: bigint
    ///  to_id: bigint
    ///  type_id: bigint
    /// }
    /// </remarks>
    internal class LinksAggregateBigIntFieldsType : ObjectGraphType
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
