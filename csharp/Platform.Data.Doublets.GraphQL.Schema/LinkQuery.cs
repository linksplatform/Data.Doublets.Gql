using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using GraphQL.Types;
using GraphQL.Validation;
using Microsoft.Extensions.DependencyInjection;
using Platform.Data.Doublets;
using Input;
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
                    new QueryArgument<LinkBooleanExpressionInputType> {Name = "where"},
                    new QueryArgument<OrderByInputType>{Name = "order_by"}
                ),
                resolve: context =>
                {
                    var links = context.RequestServices.GetService<ILinks<ulong>>();
                    var allLinks = new List<Link>();
                    var any = links.Constants.Any;
                    Link<UInt64> query = new(index: any, source: any, target: any);
                    if (context.HasArgument("where"))
                    {
                        var where = context.GetArgument<LinkBooleanExpression>("where");
                        query = new Link<UInt64>(index: any, source: (ulong?)where?.from_id?._eq ?? any, target: (ulong?)where?.to_id?._eq ?? any);
                    }
                    links.Each(link =>
                    {
                        allLinks.Add(new Link(link));
                        return links.Constants.Continue;
                    }, query);
                    if (context.HasArgument("order_by"))
                    {
                        var orderBy = context.GetArgument<OrderBy>("order_by");
                        if (orderBy.from_id == order_by.asc)
                        {
                            allLinks = allLinks.OrderBy(l => links.GetSource(new List<ulong>(){(ulong)l.id, (ulong)l.to_id, (ulong)l.from_id})).ToList();
                        }
                        else if(orderBy.from_id == order_by.desc)
                        {
                            allLinks = allLinks.OrderByDescending(l => links.GetSource(new List<ulong>() { (ulong)l.id, (ulong)l.to_id, (ulong)l.from_id })).ToList();
                        }
                    }
                    if (context.HasArgument("limit"))
                    {
                        long limit = context.GetArgument<long>("limit");
                        return allLinks.Take((int)limit);
                    }

                    return allLinks;
                });
        }
    }
}