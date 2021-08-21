using System;
using System.Collections.Generic;
using System.Linq;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using Platform.Data.Doublets;
using Input;
using GraphQL;

namespace Platform.Data.Doublets.Gql.Schema
{
    public class LinkQuery : ObjectGraphType
    {
        public static readonly QueryArguments Arguments = new QueryArguments(
                    new QueryArgument<LongGraphType> { Name = "limit" },
                    new QueryArgument<LinkBooleanExpressionInputType> { Name = "where" },
                    new QueryArgument<OrderByInputType> { Name = "order_by" },
                    new QueryArgument<LongGraphType> { Name = "offset" },
                    new QueryArgument<ListGraphType<DistinctEnum>> { Name = "distinct" }
                );
        public LinkQuery(ILinks Link)
        {
            Field<ListGraphType<LinkType>>("links",
                arguments: Arguments,
                resolve: context =>
                {
                    var links = context.RequestServices.GetService<ILinks<ulong>>();
                    return GetLinks(context, links);
                });
        }

        public static IEnumerable<Link> GetLinks(IResolveFieldContext<object> context, long? forceFromId = null, long? forceToId = null)
        {
            var links = context.RequestServices.GetService<ILinks<ulong>>();
            var any = links.Constants.Any;
            Link<UInt64> query = new(index: any, source: any, target: any);
            IEnumerable<Link> allLinks = (IEnumerable<Link>)links.All(query);
            if (context.HasArgument("where"))
            {
                var where = context.GetArgument<LinkBooleanExpression>("where");
                query = new Link<UInt64>(index: any, source: (ulong?)forceFromId ?? (ulong?)where?.from_id?._eq ?? any, target: (ulong?)forceToId ?? (ulong?)where?.to_id?._eq ?? any);
            }
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
                allLinks = allLinks.DistinctBy(GetSortSelectorAndOrderByValue(dis.First()));
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
        }
        public static IEnumerable<Link> GetLinks(IResolveFieldContext<object> context, ILinks<ulong> links, long? forceFromId = null, long? forceToId = null)
        {
            var any = links.Constants.Any;
            Link<UInt64> query = new(index: any, source: any, target: any);
            IEnumerable<Link> allLinks = (IEnumerable<Link>)links.All(query);
            if (context.HasArgument("where"))
            {
                var where = context.GetArgument<LinkBooleanExpression>("where");
                query = new Link<UInt64>(index: any, source: (ulong?)forceFromId ?? (ulong?)where?.from_id?._eq ?? any, target: (ulong?)forceToId ?? (ulong?)where?.to_id?._eq ?? any);
            }
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
                allLinks = allLinks.DistinctBy(GetSortSelectorAndOrderByValue(dis.First()));
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
        }

        private static Func<Link, long> GetSortSelectorAndOrderByValue(distinct distinct)
        {
            switch (distinct)
            {
                case distinct.from_id:
                    return x => x.from_id;
                case distinct.type_id:
                    return x => x.type_id;
                case distinct.to_id:
                    return x => x.to_id;
                default:
                    return x => x.id;
            }
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

