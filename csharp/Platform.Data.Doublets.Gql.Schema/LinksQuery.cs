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

        public static IEnumerable<Links> GetLinks(IResolveFieldContext<object> context, long? forceFromId, long? forceToId = null) => GetLinks(context, context.RequestServices.GetService<ILinks<ulong>>(), forceFromId, forceToId);

        public static IEnumerable<Links> GetLinks(IResolveFieldContext<object> context, ILinks<ulong> links, long? forceFromId = null, long? forceToId = null)
        {
            var any = links.Constants.Any;
            Link<ulong> query = new(any, any, any);
            IEnumerable<Links> allLinks;

            if (context.HasArgument("where"))
            {
                var where = context.GetArgument<LinksBooleanExpression>("where");
                if (where?.from_id?._eq != null && forceFromId != null && where.from_id._eq != forceFromId)
                {
                    return new List<Links>();
                }
                if (where?.to_id?._eq != null && forceToId != null && where.to_id._eq != forceToId)
                {
                    return new List<Links>();
                }

                // Use the deep query processor for complex where clauses
                allLinks = ProcessDeepWhereClause(links, where, forceFromId, forceToId);
            }
            else
            {
                query = new Link<ulong>(any, (ulong?)forceFromId ?? any, (ulong?)forceToId ?? any);
                allLinks = links.All(query).Select(l => new Links(l));
            }

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

        private static IEnumerable<Links> ProcessDeepWhereClause(ILinks<ulong> links, LinksBooleanExpression where, long? forceFromId = null, long? forceToId = null)
        {
            var any = links.Constants.Any;
            
            // Start with all links if no basic filters, otherwise use basic query
            IEnumerable<Links> candidateLinks;
            
            // Build basic query from simple equality conditions
            var queryId = (ulong?)where?.id?._eq ?? any;
            var queryFromId = (ulong?)forceFromId ?? (ulong?)where?.from_id?._eq ?? any;
            var queryToId = (ulong?)forceToId ?? (ulong?)where?.to_id?._eq ?? any;
            
            Link<ulong> query = new(queryId, queryFromId, queryToId);
            candidateLinks = links.All(query).Select(l => new Links(l));

            // Apply deep filtering
            return candidateLinks.Where(link => EvaluateWhereClause(links, link, where));
        }

        private static bool EvaluateWhereClause(ILinks<ulong> links, Links link, LinksBooleanExpression where)
        {
            if (where == null) return true;

            // Handle logical operators
            if (where._and != null)
            {
                return where._and.All(subWhere => EvaluateWhereClause(links, link, subWhere));
            }

            if (where._or != null)
            {
                return where._or.Any(subWhere => EvaluateWhereClause(links, link, subWhere));
            }

            if (where._not != null)
            {
                return !EvaluateWhereClause(links, link, where._not);
            }

            // Handle basic field comparisons
            if (where.id != null && !EvaluateLongComparison(link.id, where.id)) return false;
            if (where.from_id != null && !EvaluateLongComparison(link.from_id ?? 0, where.from_id)) return false;
            if (where.to_id != null && !EvaluateLongComparison(link.to_id ?? 0, where.to_id)) return false;
            if (where.type_id != null && !EvaluateLongComparison(link.type_id, where.type_id)) return false;

            // Handle nested relationship filtering
            if (where.from != null)
            {
                var fromLink = GetLinkById(links, link.from_id);
                if (fromLink == null || !EvaluateWhereClause(links, fromLink, where.from)) return false;
            }

            if (where.to != null)
            {
                var toLink = GetLinkById(links, link.to_id);
                if (toLink == null || !EvaluateWhereClause(links, toLink, where.to)) return false;
            }

            if (where.type != null)
            {
                var typeLink = GetLinkById(links, link.type_id);
                if (typeLink == null || !EvaluateWhereClause(links, typeLink, where.type)) return false;
            }

            // Handle outgoing links (where this link is the source)
            if (where.@out != null)
            {
                var outgoingLinks = GetOutgoingLinks(links, link.id);
                if (!outgoingLinks.Any(outLink => EvaluateWhereClause(links, outLink, where.@out))) return false;
            }

            // Handle incoming links (where this link is the target)
            if (where.@in != null)
            {
                var incomingLinks = GetIncomingLinks(links, link.id);
                if (!incomingLinks.Any(inLink => EvaluateWhereClause(links, inLink, where.@in))) return false;
            }

            return true;
        }

        private static bool EvaluateLongComparison(long value, LongComparisonExpression comparison)
        {
            if (comparison._eq.HasValue && value != comparison._eq.Value) return false;
            if (comparison._neq.HasValue && value == comparison._neq.Value) return false;
            if (comparison._gt.HasValue && value <= comparison._gt.Value) return false;
            if (comparison._gte.HasValue && value < comparison._gte.Value) return false;
            if (comparison._lt.HasValue && value >= comparison._lt.Value) return false;
            if (comparison._lte.HasValue && value > comparison._lte.Value) return false;
            if (comparison._in != null && !comparison._in.Contains(value)) return false;
            if (comparison._nin != null && comparison._nin.Contains(value)) return false;
            if (comparison._is_null.HasValue)
            {
                // For link IDs, we consider 0 or negative values as null
                var isNull = value <= 0;
                if (comparison._is_null.Value != isNull) return false;
            }

            return true;
        }

        private static Links GetLinkById(ILinks<ulong> links, long? linkId)
        {
            if (!linkId.HasValue || linkId.Value <= 0) return null;
            var ulongId = (ulong)linkId.Value;
            return links.Exists(ulongId) ? new Links(links.GetLink(ulongId)) : null;
        }

        private static IEnumerable<Links> GetOutgoingLinks(ILinks<ulong> links, long sourceId)
        {
            if (sourceId <= 0) return Enumerable.Empty<Links>();
            var any = links.Constants.Any;
            var query = new Link<ulong>(any, (ulong)sourceId, any);
            return links.All(query).Select(l => new Links(l));
        }

        private static IEnumerable<Links> GetIncomingLinks(ILinks<ulong> links, long targetId)
        {
            if (targetId <= 0) return Enumerable.Empty<Links>();
            var any = links.Constants.Any;
            var query = new Link<ulong>(any, any, (ulong)targetId);
            return links.All(query).Select(l => new Links(l));
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
