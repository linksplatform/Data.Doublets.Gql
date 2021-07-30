using System.Linq;
using GraphQL.Types;

namespace GraphQL.Samples.Schemas.Link
{
    public class LinkQuery : ObjectGraphType
    {
        public LinkQuery(Links Link)
        {
            Field<ListGraphType<LinkType>>("links", resolve: context => Link.AllLinks.Take(100));
        }
    }
}
