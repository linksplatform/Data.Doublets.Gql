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
                    if (context.HasArgument("limit"))
                    {
                        long receivedLink = context.GetArgument<long>("limit");
                        return Link.AllLinks.Take((int) receivedLink);
                    }

                    if (context.HasArgument("where"))
                    {
                        var allLinks = new ConcurrentStack<Link>();
                        LinkBooleanExpression arg = context.GetArgument<LinkBooleanExpression>("where");
                        var Links = (ILinks<ulong>)context.RequestServices.GetService(typeof(ILinks<ulong>));
                        Link<UInt64> query;
                        if (arg.from_id != null && arg.to_id != null)
                        {
                            if (arg.from_id._eq != 0 && arg.to_id._eq != 0)
                            {
                                query = new Link<UInt64>(index: Links.Constants.Any, source: (ulong) arg.from_id._eq,
                                    target: (ulong) arg.to_id._eq);
                                Links.Each(link =>
                                {
                                    allLinks.Push(new Link(link));
                                    return Links.Constants.Continue;
                                }, query);
                            }
                        }
                        if (arg.to_id != null)
                        {
                            if (arg.to_id._eq != 0)
                            {
                                query = new Link<UInt64>(index: Links.Constants.Any, source: Links.Constants.Any,
                                    target: (ulong) arg.to_id._eq);
                                Links.Each(link =>
                                {
                                    allLinks.Push(new Link(link));
                                    return Links.Constants.Continue;
                                }, query);
                            }
                        }

                        if (arg.from_id != null)
                        {
                            if (arg.from_id._eq != 0)
                            {
                                query = new Link<UInt64>(index: Links.Constants.Any, source: (ulong) arg.from_id._eq,
                                    target: Links.Constants.Any);
                                Links.Each(link =>
                                {
                                    allLinks.Push(new Link(link));
                                    return Links.Constants.Continue;
                                }, query);
                            }
                        }
                        if (context.HasArgument("limit"))
                        {
                            return allLinks.Take((int)context.GetArgument<long>("limit"));

                        }
                        else
                        {
                            return allLinks.Take(1);
                        }
                    }
                    return Link.AllLinks.Take(0);
                });
        }
    }
}