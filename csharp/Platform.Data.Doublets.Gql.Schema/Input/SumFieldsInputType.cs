using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gql.Samples.Schemas.Link.Input
{
    class SumFieldsInputType : InputObjectGraphType
    {
        public SumFieldsInputType()
        {
            Field<LongGraphType>("id");
            Field<LongGraphType>("from_id");
            Field<LongGraphType>("to_id");
            Field<LongGraphType>("type_id");
        }
    }
}