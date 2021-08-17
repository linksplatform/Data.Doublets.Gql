using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gql.Samples.Schemas.Link
{
    public enum distinct
    {
        from_id,
        id,
        to_id,
        type_id
    }
    class DistinctEnum : EnumerationGraphType<distinct>
    {
        public DistinctEnum()
        {
            Name = "DistinctEnum";
        }
    }
    /*enum dc_dg_links_select_column {
  """column name"""
  from_id

  """column name"""
  id

  """column name"""
  to_id

  """column name"""
  type_id
}
*/
}
