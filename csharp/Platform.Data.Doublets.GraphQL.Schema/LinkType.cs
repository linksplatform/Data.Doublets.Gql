using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Sockets;
using GraphQL.Types;
using Platform.Data.Doublets;
using Microsoft.Extensions.DependencyInjection;
using Platform.Data;

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
            return context.Source.type ?? GetLinkOrDefault(context.RequestServices.GetService(typeof(ILinks<ulong>)) , context.Source.type_id);
        }
        public static Link GetLinkOrDefault(object service, long linkid)
        {
            ILinks<ulong> Links = (ILinks<ulong>)service;
            if (Links.Exists((ulong)linkid))
            {
                var fromLink = Links.GetLink((ulong)linkid);
                return new Link()
                {
                    id = (long)Links.GetIndex(fromLink),
                    from_id = (long)Links.GetSource(fromLink),
                    to_id = (long)Links.GetTarget(fromLink)
                };
            }
            else
            {
                return null;
            }
        }
    }
}
