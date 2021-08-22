using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    class AggregateOrderByFieldInputType : InputObjectGraphType
    {
        public AggregateOrderByFieldInputType()
        {
            Field<AggregateOrderByType>("id");
            Field<AggregateOrderByType>("from_id");
            Field<AggregateOrderByType>("to_id");
            Field<AggregateOrderByType>("type_id");
        }
    }
}
