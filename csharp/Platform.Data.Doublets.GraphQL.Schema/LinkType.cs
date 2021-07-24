using LinksStorage;
using GraphQL.Types;

namespace GraphQL.Samples.Schemas.Chat
{
    public class LinkType : ObjectGraphType<Link>
    {
        public LinkType()
        {
            Field(o => o.Id);
            Field(o => o.from_id);
            Field(o => o.from, type: typeof(LinkType));
            Field(o => o.to, type: typeof(LinkType));
            Field(o => o.to_id);
            Field(o => o.type, type: typeof(LinkType));
            Field(o => o.type_id);
        }

        //private MessageFrom ResolveFrom(IResolveFieldContext<Message> context)
        //{
        //    var message = context.Source;
        //    return message.From;
        //}
    }
}
