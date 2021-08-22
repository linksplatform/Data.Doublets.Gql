using GraphQL.Types;
using Platform.Data.Doublets.Gql.Schema.Types;
using Platform.Data.Doublets.Gql.Schema.Types.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gql.Samples.Schemas.Link.Types
{
    class LinksAggregateType : ObjectGraphType
    {
        public LinksAggregateType()
        {
            Field<LinksAggregateFieldsType>("aggregate_fields");
            Field<ListGraphType<LinkType>>("nodes");
        }
    }
}
