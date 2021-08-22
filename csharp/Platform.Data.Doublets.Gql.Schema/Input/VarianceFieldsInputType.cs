using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gql.Samples.Schemas.Link.Input
{
    class VarianceFieldsInputType : InputObjectGraphType
    {
        public VarianceFieldsInputType()
        {
            Field<FloatGraphType>("id");
            Field<FloatGraphType>("from_id");
            Field<FloatGraphType>("to_id");
            Field<FloatGraphType>("type_id");
        }
    }
}
