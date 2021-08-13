using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
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
                    new QueryArgument<LongGraphType> { Name = "limit" },
                    new QueryArgument<LinkBooleanExpressionInputType> { Name = "where" },
                    new QueryArgument<OrderByInputType> { Name = "order_by" },
                    new QueryArgument<LongGraphType> { Name = "offset"},
                    new QueryArgument<ListGraphType<DistinctEnum>> { Name = "distinct"}
                ),
                resolve: context =>
                {
                    var links = context.RequestServices.GetService<ILinks<ulong>>();
                    var linksList = new List<Link>();
                    IEnumerable<Link> allLinks = linksList;
                    var any = links.Constants.Any;
                    Link<UInt64> query = new(index: any, source: any, target: any);
                    if (context.HasArgument("where"))
                    {
                        var where = context.GetArgument<LinkBooleanExpression>("where");
                        query = new Link<UInt64>(index: any, source: (ulong?)where?.from_id?._eq ?? any, target: (ulong?)where?.to_id?._eq ?? any);
                    }
                    links.Each(link =>
                    {
                        linksList.Add(new Link(link));
                        return links.Constants.Continue;
                    }, query);
                    if (context.HasArgument("order_by"))
                    {
                        var orderBy = context.GetArgument<OrderBy>("order_by");
                        Func<Func<Link, long>, IOrderedEnumerable<Link>> orderer = allLinks.OrderByDescending;
                        GetSelectorAndOrderByValue(orderBy, out Func<Link, long> selector, out order_by? orderByValue);
                        if (orderByValue == order_by.asc)
                        {
                            orderer = allLinks.OrderBy;
                        }
                        allLinks = orderer(selector);
                    }
                    if (context.HasArgument("distinct"))
                    {
                        var dis = context.GetArgument<List<distinct>>("distinct");
                        switch (dis.First())
                        {
                            case distinct.from_id:
                                allLinks = allLinks.DistinctBy(x => x.from_id);
                                break;
                            case distinct.type_id:
                                allLinks = allLinks.DistinctBy(x => x.type_id);
                                break;
                            case distinct.to_id:
                                allLinks = allLinks.DistinctBy(x => x.to_id);
                                break;
                            default:
                                allLinks = allLinks.DistinctBy(x => x.id);
                                break;
                        }
                    }
                    if (context.HasArgument("offset"))
                    {
                        int offset = context.GetArgument<int>("offset");
                        allLinks = allLinks.Skip(offset);
                    }
                    if (context.HasArgument("limit"))
                    {
                        long limit = context.GetArgument<long>("limit");
                        return allLinks.Take((int)limit);
                    }

                    return allLinks;
                });

        }

        private static void GetSelectorAndOrderByValue(OrderBy orderBy, out Func<Link, long> selector, out order_by? orderByValue)
        {
            orderByValue = orderBy.from_id;
            if (orderByValue != null)
            {
                selector = l => l.from_id;
                return;
            }
            orderByValue = orderBy.to_id;
            if (orderByValue != null)
            {
                selector = l => l.to_id;
                return;
            }
            orderByValue = orderBy.type_id;
            if (orderByValue != null)
            {
                selector = l => l.type_id;
                return;
            }
            orderByValue = orderBy.id;
            selector = l => l.id;
        }
    }
}
