﻿using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gql.Samples.Schemas.Link.Input
{
    class AggregateOrderByFieldInputType : InputObjectGraphType
    {
        public AggregateOrderByFieldInputType()
        {
            Field<AggregateOrderByInputType>("id");
            Field<AggregateOrderByInputType>("from_id");
            Field<AggregateOrderByInputType>("to_id");
            Field<AggregateOrderByInputType>("type_id");
        }
    }
}
