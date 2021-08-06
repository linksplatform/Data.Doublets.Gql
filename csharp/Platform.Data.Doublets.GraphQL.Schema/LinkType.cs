using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Sockets;
using GraphQL.Types;
using Platform.Data.Doublets;
using Microsoft.Extensions.DependencyInjection;
using Platform.Data;
using Platform.Memory;

namespace GraphQL.Samples.Schemas.Link
{
    public class LinkType : ObjectGraphType<Link>
    {
        public LinkType()
        {
            Field(o => o.id);
            Field(o => o.from_id);
            Field(o => o.from, false, type: typeof(LinkType)).Resolve(ResolveFrom);
            Field(o => o.to, type: typeof(LinkType)).Resolve(ResolveTo);
            Field(o => o.to_id);
            Field(o => o.type, type: typeof(LinkType)).Resolve(ResolveType);
            Field(o => o.type_id);
            Field<ListGraphType<LinkType>>().Name("in").Resolve(ResolveIn);
            Field<ListGraphType<LinkType>>().Name("out").Resolve(ResolveOut);
        }

        private List<Link> ResolveIn(IResolveFieldContext<Link> context)
        {
            var inList = new List<Link>();
            ILinks<ulong> Links = (ILinks<ulong>) context.RequestServices.GetService(typeof(ILinks<ulong>));
            var query = new Link<UInt64>(index: Links.Constants.Any, source: Links.Constants.Any, target: (ulong)context.Source.id);
            Links.Each(link =>
            {
                inList.Add(new Link(link));
                return Links.Constants.Continue;
            }, query);
            return inList;
        }
        private List<Link> ResolveOut(IResolveFieldContext<Link> context)
        {
            var outList = new List<Link>();
            ILinks<ulong> Links = (ILinks<ulong>)context.RequestServices.GetService(typeof(ILinks<ulong>));
            var query = new Link<UInt64>(index: Links.Constants.Any, source: (ulong)context.Source.id ,target: Links.Constants.Any);
            Links.Each(link =>
            {
                outList.Add(new Link(link));
                return Links.Constants.Continue;
            }, query);
            return outList;
        }

        private Link ResolveFrom(IResolveFieldContext<Link> context)
        {
            return context.Source.from ?? GetLinkOrDefault(context.RequestServices.GetService(typeof(ILinks<ulong>)), context.Source.from_id);
        }

        private Link ResolveTo(IResolveFieldContext<Link> context)
        {
            return context.Source.to ?? GetLinkOrDefault(context.RequestServices.GetService(typeof(ILinks<ulong>)), context.Source.to_id);
        }

        private Link ResolveType(IResolveFieldContext<Link> context)
        {
            return context.Source.type ?? GetLinkOrDefault(context.RequestServices.GetService(typeof(ILinks<ulong>)),
                context.Source.type_id);
        }

        public static Link GetLinkOrDefault(object service, long linkId)
        {
           return GetLinkOrDefault((ILinks<ulong>)service, (ulong)linkId);
        }
        public static Link GetLinkOrDefault(ILinks<ulong> links, ulong link)
        {
            if (links.Exists((ulong)link))
            {
                var fromLink = links.GetLink((ulong)link);
                return new Link(fromLink);
            }
            else
            {
                return null;
            }
        }
    }
}
