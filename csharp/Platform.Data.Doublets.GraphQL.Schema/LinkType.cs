using System.Net.Http.Headers;
using LinksStorage;
using GraphQL.Types;
using Platform.Data.Doublets;

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
            return link.from ?? new Link();
        }

        private Link ResolveTo(IResolveFieldContext<Link> context)
        {
            var link = context.Source;
            return link.to ?? new Link();
        }
        private Link ResolveType(IResolveFieldContext<Link> context)
        {
            var link = context.Source;
            return link.type ?? new Link();
        }
    }
}
