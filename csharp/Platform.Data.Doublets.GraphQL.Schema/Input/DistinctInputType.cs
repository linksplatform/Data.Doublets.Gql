using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Samples.Schemas.Link.Input
{
    class DistinctInputType : InputObjectGraphType<DistinctEnum>
    {
        public DistinctInputType()
        {
            Field<DistinctEnum>("distinct");
        }
    }
}
