using GraphQL;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using Platform.Data.Doublets.Gql.Schema.Types;
using Platform.Data.Doublets.Gql.Schema.Types.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using Platform.Data.Doublets.Gql.Schema.Types.Enum;

namespace Platform.Data.Doublets.Gql.Schema
{
    /// <remarks>
    /// """query root"""
    /// type query_root {
    ///   """
    ///   fetch data from the table: "links"
    ///   """
    ///   links(
    ///     """distinct select on columns"""
    ///     distinct_on: [links_select_column!]
    ///
    ///     """limit the number of rows returned"""
    ///     limit: Int
    ///
    ///     """skip the first n rows. Use only with order_by"""
    ///     offset: Int
    ///
    ///     """sort the rows by one or more columns"""
    ///     order_by: [links_order_by!]
    ///
    ///     """filter the rows returned"""
    ///     where: links_bool_exp
    ///   ): [links!]!
    ///
    ///   """
    ///   fetch aggregated fields from the table: "links"
    ///   """
    ///   links_aggregate(
    ///     """distinct select on columns"""
    ///     distinct_on: [links_select_column!]
    ///
    ///     """limit the number of rows returned"""
    ///     limit: Int
    ///
    ///     """skip the first n rows. Use only with order_by"""
    ///     offset: Int
    ///
    ///     """sort the rows by one or more columns"""
    ///     order_by: [links_order_by!]
    ///
    ///     """filter the rows returned"""
    ///     where: links_bool_exp
    ///   ): links_aggregate!
    ///
    ///   """fetch data from the table: "links" using primary key columns"""
    ///   links_by_pk(id: bigint!): links
    /// }
    /// </remarks>
    public class LinksQuery : ObjectGraphType
    {
        public static readonly QueryArguments Arguments = new (
                    new QueryArgument<ListGraphType<LinksColumnEnumType>> { Name = "distinct_on" },
                    new QueryArgument<LongGraphType> { Name = "limit" },
                    new QueryArgument<LongGraphType> { Name = "offset" },
                    new QueryArgument<LinksOrderByInputType> { Name = "order_by" },
                    new QueryArgument<LinksBooleanExpressionInputType> { Name = "where" }
        );
        public LinksQuery(ILinks<ulong> links) => Field<ListGraphType<LinksType>>("links",
                arguments: Arguments,
                resolve: context =>
                {
                    return GetLinks(context, links);
                });

        public static IEnumerable<Link> GetLinks(IResolveFieldContext<object> context, long? forceFromId = null, long? forceToId = null) => GetLinks(context, context.RequestServices.GetService<ILinks<ulong>>(), forceFromId, forceToId);
        public static IEnumerable<Link> GetLinks(IResolveFieldContext<object> context, ILinks<ulong> links, long? forceFromId = null, long? forceToId = null)
        {
            var any = links.Constants.Any;
            Link<ulong> query = new(index: any, source: any, target: any);
            if (context.HasArgument("where"))
            {
                var where = context.GetArgument<LinksBooleanExpression>("where");
                if(where?.from_id._eq != null && forceFromId != null && where.from_id._eq != forceFromId)
                {
                    return new List<Link>();
                }
                if (where?.to_id._eq != null && forceToId != null && where.to_id._eq != forceToId)
                {
                    return new List<Link>();
                }
                query = new Link<ulong>(index: (ulong?)where?.id?._eq ?? any, source: (ulong?)forceFromId ?? (ulong?)where?.from_id?._eq ?? any, target: (ulong?)forceToId ?? (ulong?)where?.to_id?._eq ?? any);
            }
            var allLinks = links.All(query).Select(l => new Link(l));
            if (context.HasArgument("order_by"))
            {
                GetSelectorAndOrderByValue(context.GetArgument<LinksOrderBy>("order_by"), out var selector, out var orderByValue);
                allLinks = orderByValue == order_by.asc ? allLinks.OrderBy(selector) : allLinks.OrderByDescending(selector);
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

        private static Func<Link, long> GetSortSelectorAndOrderByValue(LinksColumn distinct)
        {
            switch (distinct)
            {
                case LinksColumn.from_id:
                    return x => x.from_id;
                case LinksColumn.type_id:
                    return x => x.type_id;
                case LinksColumn.to_id:
                    return x => x.to_id;
                default:
                    return x => x.id;
            }
        }

        private static void GetSelectorAndOrderByValue(LinksOrderBy orderBy, out Func<Link, long> selector, out order_by? orderByValue)
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

