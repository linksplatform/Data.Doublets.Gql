using System.Collections.Generic;
using System.Linq;
using GraphQL.Types;
using static GraphQL.Samples.Schemas.Link.Link;

namespace GraphQL.Samples.Schemas.Link
{
    public class LinkQuery : ObjectGraphType
    {
        public LinkQuery(ILinks Link)
        {
            Field<ListGraphType<LinkType>>("links", resolve: context => Link.AllLinks.Take(100));
        }
    }
}
