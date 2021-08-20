using GraphQL.Types;
using Platform.Data.Doublets.Gql.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Input
{
    class LinksMutationResponseType : ObjectGraphType
    {
        public LinksMutationResponseType()
        {
            Field<IntGraphType>("affected_rows");
            Field<ListGraphType<LinkType>>("returning");
        }
    }
}
