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
                    new QueryArgument<LongGraphType> {Name = "limit"},
                    new QueryArgument<LinkBooleanExpressionInputType> {Name = "where"},
                    new QueryArgument<OrderByInputType>{Name = "order_by"}
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
                        Func<Link, long> idKeySelector = l => l.id;
                        Func<Link, long> fromIdKeySelector = l => l.from_id;
                        Func<Link, long> toIdSelector = l => l.to_id;
                        Func<Link, long> typeIdSelector = l => l.type_id;
                        Func<Func<Link, long>, IOrderedEnumerable<Link>> orderAscending = allLinks.OrderBy;
                        Func<Func<Link, long>, IOrderedEnumerable<Link>> orderDecending = allLinks.OrderByDescending;
                        var orderBy = context.GetArgument<OrderBy>("order_by");
                        Func<Link, long> selector = idKeySelector;
                        Func<Func<Link, long>, IOrderedEnumerable<Link>> orderer = orderAscending;
                        if (orderBy.from_id == order_by.asc || orderBy.to_id == order_by.asc || orderBy.id == order_by.asc || orderBy.type_id == order_by.asc)
                        {
                            orderer = orderAscending;
                        }
                        else
                        {
                            orderer = orderDecending;
                        }
                        if (orderBy.from_id != null)
                        {
                            selector = fromIdKeySelector;
                        }

                        if (orderBy.to_id != null)
                        {
                            selector = toIdSelector;
                        }

                        if (orderBy.id != null)
                        {
                            selector = idKeySelector;
                        }
                        if (orderBy.type_id != null)
                        {
                            selector = typeIdSelector;
                        }
                        allLinks = Sort(selector, orderer);
                    }

                    if (context.HasArgument("limit"))
                    {
                        long limit = context.GetArgument<long>("limit");
                        return allLinks.Take((int)limit);
                    }

                    return allLinks;
                });
        }

        private IOrderedEnumerable<Link> Sort(Func<Link, long> selector, Func<Func<Link, long>, IOrderedEnumerable<Link>> orderer) => orderer(selector);
    }
}