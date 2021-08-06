using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using GraphQL.Types;
using GraphQL.Validation;
using Platform.Data.Doublets;
using static GraphQL.Samples.Schemas.Link.Link;

namespace GraphQL.Samples.Schemas.Link
{
    public class LinkQuery : ObjectGraphType
    {
        public LinkQuery(ILinks Link)
        {
            Field<ListGraphType<LinkType>>("links",
                arguments: new QueryArguments(
                    new QueryArgument<LongGraphType> {Name = "limit"},
                    new QueryArgument<LinkBooleanExpressionInputType> {Name = "where"}
                ),
                resolve: context =>
                {
                    var Links = (ILinks<ulong>)context.RequestServices.GetService(typeof(ILinks<ulong>));
                    var allLinks = new List<Link>();
                    if (context.HasArgument("where"))
                    {
                        LinkBooleanExpression arg = context.GetArgument<LinkBooleanExpression>("where");
                        Link<UInt64> query;
                        query = new Link<UInt64>(index: Links.Constants.Any, source: arg?.from_id?._eq != null ?
                        (ulong) arg.from_id._eq : Links.Constants.Any, target: arg?.to_id?._eq != null ? (ulong) arg.to_id._eq : Links.Constants.Any);
                        Links.Each(link =>
                        {
                            allLinks.Add(new Link(link));
                            return Links.Constants.Continue;
                        }, query);
                    }
                    else
                    {
                        var query = new Link<UInt64>(index: Links.Constants.Any, source: Links.Constants.Any, target: Links.Constants.Any);
                        Links.Each(link =>
                        {
                            allLinks.Add(new Link(link));
                            return Links.Constants.Continue;
                        }, query);
                    }
                    if (context.HasArgument("limit"))
                    {
                        long receivedLink = context.GetArgument<long>("limit");
                        return allLinks.Take((int)receivedLink);
                    }

                    return allLinks;
                });
        }
    }
}