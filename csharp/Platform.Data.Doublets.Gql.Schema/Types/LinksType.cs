using GraphQL;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace Platform.Data.Doublets.Gql.Schema.Types
{
    using MappedType = Links;

    public class LinksType : ObjectGraphType<MappedType>
    {
        public LinksType()
        {
            Name = "links";
            Description = "columns and relationships of \"links\"";
            Field(o => o.from, true, typeof(LinksType)).Resolve(ResolveFrom);
            Field<NonNullGraphType<LongGraphType>>(nameof(MappedType.from_id));
            Field<NonNullGraphType<LongGraphType>>(nameof(MappedType.id));
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<LinksType>>>>(nameof(MappedType.@in), null, LinksQuery.Arguments, ResolveIn);
            Field<NonNullGraphType<LinksAggregateType>>(nameof(MappedType.in_aggregate), null, LinksQuery.Arguments, ResolveInAggregate);
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<LinksType>>>>(nameof(MappedType.@out), null, LinksQuery.Arguments, ResolveOut);
            Field<NonNullGraphType<LinksAggregateType>>(nameof(MappedType.out_aggregate), null, LinksQuery.Arguments, ResolveOutAggregate);
            Field(o => o.to, true, typeof(LinksType)).Resolve(ResolveTo);
            Field<NonNullGraphType<LongGraphType>>(nameof(MappedType.to_id));
            Field(o => o.type, true, typeof(LinksType)).Resolve(ResolveType);
            Field<NonNullGraphType<LongGraphType>>(nameof(MappedType.type_id));
        }

        private LinksAggregateType ResolveInAggregate(IResolveFieldContext<Links> context) => new();

        private LinksAggregateType ResolveOutAggregate(IResolveFieldContext<Links> context) => new();

        private List<Links> ResolveIn(IResolveFieldContext<Links> context) => LinksQuery.GetLinks(context, null, context.Source.id).ToList();

        private List<Links> ResolveOut(IResolveFieldContext<Links> context) => LinksQuery.GetLinks(context, context.Source.id).ToList();

        private Links ResolveFrom(IResolveFieldContext<Links> context) => context.Source.from ?? GetLinkOrDefault(context, context.Source.from_id);

        private Links ResolveTo(IResolveFieldContext<Links> context) => context.Source.to ?? GetLinkOrDefault(context, context.Source.to_id);

        private Links ResolveType(IResolveFieldContext<Links> context) => context.Source.type ?? GetLinkOrDefault(context, (long?)context.Source.type_id);

        public static Links GetLinkOrDefault(IResolveFieldContext<Links> context, long? linkId) => linkId != null ? GetLinkOrDefault(context.RequestServices.GetService<ILinks<ulong>>(), (ulong)linkId) : default;

        public static Links GetLinkOrDefault(object service, long linkId) => GetLinkOrDefault((ILinks<ulong>)service, (ulong)linkId);

        public static Links GetLinkOrDefault(ILinks<ulong> links, ulong link) => links.Exists(link) ? new Links(links.GetLink(link)) : null;
    }
}
