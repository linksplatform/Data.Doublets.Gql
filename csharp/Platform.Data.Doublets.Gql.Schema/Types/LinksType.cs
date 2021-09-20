using GraphQL;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace Platform.Data.Doublets.Gql.Schema.Types
{
    /// <remarks>
    /// """
    /// columns and relationships of "links"
    /// """
    /// type links {
    ///   """An object relationship"""
    ///   from: links
    ///   from_id: bigint!
    ///   id: bigint!
    ///
    ///   """An array relationship"""
    ///   in(
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
    ///   """An aggregated array relationship"""
    ///   in_aggregate(
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
    ///   """An array relationship"""
    ///   out(
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
    ///   """An aggregated array relationship"""
    ///   out_aggregate(
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
    ///   """An object relationship"""
    ///   to: links
    ///   to_id: bigint!
    ///
    ///   """An object relationship"""
    ///   type: links
    ///   type_id: bigint!
    /// }
    /// </remarks>
    public class LinksType : ObjectGraphType<Link>
    {
        public LinksType()
        {
            Name = "links";
            Field(o => o.from, nullable: true, type: typeof(LinksType)).Resolve(ResolveFrom);
            Field<LongGraphType>("from_id");
            Field<LongGraphType>("id");
            Field<ListGraphType<LinksType>>("in", null, LinksQuery.Arguments, ResolveIn, null);
            Field<LinksAggregateType>("in_aggregate", null, LinksQuery.Arguments, ResolveInAggregate, null);
            Field<ListGraphType<LinksType>>("out", null, LinksQuery.Arguments, ResolveOut, null);
            Field<LinksAggregateType>("out_aggregate", null, LinksQuery.Arguments, ResolveOutAggregate, null);
            Field(o => o.to, nullable: true, type: typeof(LinksType)).Resolve(ResolveTo);
            Field<LongGraphType>("to_id");
            Field(o => o.type, nullable: true, type: typeof(LinksType)).Resolve(ResolveType);
            Field<LongGraphType>("type_id");
        }
        private LinksAggregateType ResolveInAggregate(IResolveFieldContext<Link> context) => new();

        private LinksAggregateType ResolveOutAggregate(IResolveFieldContext<Link> context) => new();

        private List<Link> ResolveIn(IResolveFieldContext<Link> context) => LinksQuery.GetLinks(context, null, context.Source.id).ToList();

        private List<Link> ResolveOut(IResolveFieldContext<Link> context) => LinksQuery.GetLinks(context, context.Source.id, null).ToList();

        private Link ResolveFrom(IResolveFieldContext<Link> context) => context.Source.@from ?? GetLinkOrDefault(context, context.Source.from_id);

        private Link ResolveTo(IResolveFieldContext<Link> context) => context.Source.to ?? GetLinkOrDefault(context, context.Source.to_id);

        private Link ResolveType(IResolveFieldContext<Link> context) => context.Source.type ?? GetLinkOrDefault(context, context.Source.type_id);

        public static Link GetLinkOrDefault(IResolveFieldContext<Link> context, long linkId) => GetLinkOrDefault(context.RequestServices.GetService<ILinks<ulong>>(), (linkId));

        public static Link GetLinkOrDefault(object service, long linkId) => GetLinkOrDefault((ILinks<ulong>)service, (ulong)linkId);

        public static Link GetLinkOrDefault(ILinks<ulong> links, ulong link) => links.Exists(link) ? new Link(links.GetLink(link)) : null;
    }
}
