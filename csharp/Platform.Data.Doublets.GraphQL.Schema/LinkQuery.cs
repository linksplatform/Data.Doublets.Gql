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
                        if (arg?.from_id?._eq != null && arg?.to_id?._eq != null)
                        {
                            query = new Link<UInt64>(index: Links.Constants.Any, source: (ulong) arg.from_id._eq,
                                target: (ulong) arg.to_id._eq);
                            Links.Each(link =>
                            {
                                allLinks.Add(new Link(link));
                                return Links.Constants.Continue;
                            }, query);
                        }

                        if (arg?.to_id?._eq != null)
                        {
                            query = new Link<UInt64>(index: Links.Constants.Any, source: Links.Constants.Any,
                                target: (ulong) arg.to_id._eq);
                            Links.Each(link =>
                            {
                                allLinks.Add(new Link(link));
                                return Links.Constants.Continue;
                            }, query);
                        }

                        if (arg?.from_id?._eq != null)
                        {
                            query = new Link<UInt64>(index: Links.Constants.Any, source: (ulong) arg.from_id._eq,
                                target: Links.Constants.Any);
                            Links.Each(link =>
                            {
                                allLinks.Add(new Link(link));
                                return Links.Constants.Continue;
                            }, query);
                        }
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