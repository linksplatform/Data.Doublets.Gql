using GraphQL.Types;
using Platform.Data.Doublets.Gql.Schema.Types.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gql.Samples.Schemas.Link.Types
{
    class AggregateType
    {
        public AggregateOrderByFieldInputType aggregate_fields { get; set; }

        public List<Platform.Data.Doublets.Gql.Schema.Link> nodes { get; set; }
    }
}
