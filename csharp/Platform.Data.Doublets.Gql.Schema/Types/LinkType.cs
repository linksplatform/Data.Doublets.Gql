using GraphQL;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace Platform.Data.Doublets.Gql.Schema.Types
{
    public class LinkType : ObjectGraphType<Link>
    {
        public LinkType()
        {
            Field(o => o.id);
            Field(o => o.from_id, nullable: true);
            Field(o => o.from, type: typeof(LinkType)).Resolve(ResolveFrom);
            Field(o => o.to, type: typeof(LinkType)).Resolve(ResolveTo);
            Field(o => o.to_id);
            Field(o => o.type, type: typeof(LinkType)).Resolve(ResolveType);
            Field(o => o.type_id);
            Field<ListGraphType<LinkType>>("in", null, LinkQuery.Arguments, ResolveIn, null);
            Field<ListGraphType<LinkType>>("out", null, LinkQuery.Arguments, ResolveOut, null);
            Field<LinksAggregateType>("in_aggregate", null, LinkQuery.Arguments, ResolveInAggregate, null);
            Field<LinksAggregateType>("out_aggregate", null, LinkQuery.Arguments, ResolveOutAggregate, null);
        }
        private LinksAggregateType ResolveInAggregate(IResolveFieldContext<Link> context) => new();

        private LinksAggregateType ResolveOutAggregate(IResolveFieldContext<Link> context) => new();

        private List<Link> ResolveIn(IResolveFieldContext<Link> context) => LinkQuery.GetLinks(context, null, context.Source.id).ToList();

        private List<Link> ResolveOut(IResolveFieldContext<Link> context) => LinkQuery.GetLinks(context, context.Source.id, null).ToList();

        private Link ResolveFrom(IResolveFieldContext<Link> context) => context.Source.@from ?? GetLinkOrDefault(context, context.Source.from_id);

        private Link ResolveTo(IResolveFieldContext<Link> context) => context.Source.to ?? GetLinkOrDefault(context, context.Source.to_id);

        private Link ResolveType(IResolveFieldContext<Link> context) => context.Source.type ?? GetLinkOrDefault(context, context.Source.type_id);

        public static Link GetLinkOrDefault(IResolveFieldContext<Link> context, long linkId) => GetLinkOrDefault(context.RequestServices.GetService<ILinks<ulong>>(), (linkId));

        public static Link GetLinkOrDefault(object service, long linkId) => GetLinkOrDefault((ILinks<ulong>)service, (ulong)linkId);

        public static Link GetLinkOrDefault(ILinks<ulong> links, ulong link) => links.Exists(link) ? new Link(links.GetLink(link)) : null;
    }
}
