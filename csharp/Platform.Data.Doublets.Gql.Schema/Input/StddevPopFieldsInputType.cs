using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Data.Doublets.Gql.Schema.Input
{
    class StddevPopFieldsInputType : InputObjectGraphType
    {
        public StddevPopFieldsInputType()
        {
            Field<FloatGraphType>("id");
            Field<FloatGraphType>("from_id");
            Field<FloatGraphType>("to_id");
            Field<FloatGraphType>("type_id");
        }
    }
}
