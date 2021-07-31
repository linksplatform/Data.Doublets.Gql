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
            Field<ListGraphType<LinkType>>("links",
                arguments: new QueryArguments(
                    new QueryArgument<LongGraphType> { Name = "limit" }
                ),
                resolve: context =>
                {
                    long receivedLink = context.GetArgument<long>("limit");
                    if (context.HasArgument("limit"))
                    {
                        return Link.AllLinks.Take((int)receivedLink);
                    }
                    return Link.AllLinks.Take(0);
                });
        }
    }
}