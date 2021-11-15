using GraphQL;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using Platform.Data.Doublets.Gql.Schema.Enums;
using Platform.Data.Doublets.Gql.Schema.Types;
using Platform.Data.Doublets.Gql.Schema.Types.Enums;
using Platform.Data.Doublets.Gql.Schema.Types.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Platform.Data.Doublets.Gql.Schema
{
    public class LinksQuery : ObjectGraphType
    {
        public static readonly QueryArguments Arguments = new(new QueryArgument<ListGraphType<NonNullGraphType<LinksSelectColumnEnumBaseType>>> { Name = "distinct_on" }, new QueryArgument<IntGraphType> { Name = "limit" }, new QueryArgument<IntGraphType> { Name = "offset" }, new QueryArgument<ListGraphType<NonNullGraphType<LinksOrderByInputType>>> { Name = "order_by" }, new QueryArgument<LinksBooleanExpressionInputType> { Name = "where" });

        public LinksQuery(ILinks<ulong> links)
        {
            Name = "query_root";
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<LinksType>>>>("links", arguments: Arguments, resolve: context => { return GetLinks(context, links); });
            Field<NonNullGraphType<LinksAggregateType>>("links_aggregate", arguments: Arguments, resolve: context => "");
            Field<LinksType>("links_by_pk", arguments: new QueryArguments(new QueryArgument<NonNullGraphType<LongGraphType>> { Name = "id" }));
        }

        public static IEnumerable<Links> GetLinks(IResolveFieldContext<object> context) => GetLinks(context, context.RequestServices.GetService<ILinks<ulong>>());

        public static IEnumerable<Links> GetLinks(IResolveFieldContext<object> context, long? forceFromId) => GetLinks(context, context.RequestServices.GetService<ILinks<ulong>>(), forceFromId);

        public static IEnumerable<Links> GetLinks(IResolveFieldContext<object> context, ILinks<ulong> links, long? forceFromId = null, long? forceToId = null)
        {
            var any = links.Constants.Any;
            Link<ulong> query = new(any, any, any);
            if (context.HasArgument("where"))
            {
                var where = context.GetArgument<LinksBooleanExpression>("where");
                if (where?.from_id._eq != null && forceFromId != null && where.from_id._eq != forceFromId)
                {
                    return new List<Links>();
                }
                if (where?.to_id._eq != null && forceToId != null && where.to_id._eq != forceToId)
                {
                    return new List<Links>();
                }
                query = new Link<ulong>((ulong?)where?.id?._eq ?? any, (ulong?)forceFromId ?? (ulong?)where?.from_id?._eq ?? any, (ulong?)forceToId ?? (ulong?)where?.to_id?._eq ?? any);
            }
            var allLinks = links.All(query).Select(l => new Links(l));
            if (context.HasArgument("order_by"))
            {
                GetSelectorAndOrderByValue(context.GetArgument<List<LinksOrderBy>>("order_by").Single(), out var selector, out var orderByValue);
                allLinks = orderByValue == OrderBy.asc ? allLinks.OrderBy(selector) : allLinks.OrderByDescending(selector);
            }
            if (context.HasArgument("distinct"))
            {
                var distinct = context.GetArgument<List<LinksColumn>>("distinct");
                allLinks = allLinks.DistinctBy(GetSortSelectorAndOrderByValue(distinct.First()));
            }
            if (context.HasArgument("offset"))
            {
                var offset = context.GetArgument<int>("offset");
                allLinks = allLinks.Skip(offset);
            }
            if (context.HasArgument("limit"))
            {
                var limit = context.GetArgument<long>("limit");
                return allLinks.Take((int)limit);
            }
            return allLinks;
        }

        private static Func<Links, long> GetSortSelectorAndOrderByValue(LinksColumn distinct)
        {
            switch (distinct)
            {
                case LinksColumn.from_id:
                    return x => (long)x.from_id;
                case LinksColumn.type_id:
                    return x => x.type_id;
                case LinksColumn.to_id:
                    return x => (long)x.to_id;
                default:
                    return x => x.id;
            }
        }

        private static void GetSelectorAndOrderByValue(LinksOrderBy orderBy, out Func<Links, long> selector, out OrderBy? orderByValue)
        {
            orderByValue = orderBy.from_id;
            if (orderByValue != null)
            {
                selector = l => (long)l.from_id;
                return;
            }
            orderByValue = orderBy.to_id;
            if (orderByValue != null)
            {
                selector = l => (long)l.to_id;
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
