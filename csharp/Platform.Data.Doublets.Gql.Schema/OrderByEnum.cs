using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Data.Doublets.Gql.Schema
{
    class OrderByEnum : EnumerationGraphType<order_by>
    {
        public OrderByEnum()
        {
            Name = "OrderByEnum";
        }
    }
}
