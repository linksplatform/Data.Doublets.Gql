using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Sockets;
using LinksStorage;
using GraphQL.Types;
using Platform.Data.Doublets;
using Microsoft.Extensions.DependencyInjection;
using Platform.Data;

namespace GraphQL.Samples.Schemas.Chat
{
    public class LinkType : ObjectGraphType<Link>
    {
        public LinkType()
        {
            Field(o => o.Id);
            Field(o => o.from_id);
            Field(o => o.from, false, type: typeof(LinkType)).Resolve(ResolveFrom);
            Field(o => o.to, type: typeof(LinkType)).Resolve(ResolveTo);
            Field(o => o.to_id);
            Field(o => o.type, type: typeof(LinkType)).Resolve(ResolveType);
            Field(o => o.type_id);
        }

        private Link ResolveFrom(IResolveFieldContext<Link> context)
        {
            var link = context.Source;
            if (link.from != null)
            {
                return link.from;
            }
            else
            {
                var service = context.RequestServices.GetService(typeof(ILinks<ulong>));
                ILinks<ulong> Links = (ILinks<ulong>) service;
                if (Links.Exists((ulong) link.to_id))
                {
                    var fromLink = Links.GetLink((ulong) link.from_id);
                    return new Link()
                    {
                        Id = (long) Links.GetIndex(fromLink),
                        from_id = (long) Links.GetSource(fromLink),
                        to_id = (long) Links.GetTarget(fromLink)
                    };
                }
                else
                {
                    return null;
                }
            }
        }

        private Link ResolveTo(IResolveFieldContext<Link> context)
        {
            var link = context.Source;
            if (link.from != null)
            {
                return link.from;
            }
            else
            {
                var service = context.RequestServices.GetService(typeof(ILinks<ulong>));
                ILinks<ulong> Links = (ILinks<ulong>) service;
                if (Links.Exists((ulong) link.to_id))
                {
                    var fromLink = Links.GetLink((ulong) link.to_id);
                    return new Link()
                    {
                        Id = (long) Links.GetIndex(fromLink),
                        from_id = (long) Links.GetSource(fromLink),
                        to_id = (long) Links.GetTarget(fromLink)
                    };
                }
                else
                {
                    return null;
                }
            }
        }

        private Link ResolveType(IResolveFieldContext<Link> context)
        {
            var link = context.Source;
            if (link.from != null)
            {
                return link.from;
            }
            else
            {
                var service = context.RequestServices.GetService(typeof(ILinks<ulong>));
                ILinks<ulong> Links = (ILinks<ulong>) service;
                if (Links.Exists((ulong) link.type_id))
                {
                    var fromLink = Links.GetLink((ulong) link.type_id);
                    return new Link()
                    {
                        Id = (long) Links.GetIndex(fromLink),
                        from_id = (long) Links.GetSource(fromLink),
                        to_id = (long) Links.GetTarget(fromLink)
                    };
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
