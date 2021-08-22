using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    class LinksFieldsOrderByInputType : InputObjectGraphType
    {
        public LinksFieldsOrderByInputType()
        {
            Field<OrderByInputType>("id");
            Field<OrderByInputType>("from_id");
            Field<OrderByInputType>("to_id");
            Field<OrderByInputType>("type_id");
        }
    }
}
