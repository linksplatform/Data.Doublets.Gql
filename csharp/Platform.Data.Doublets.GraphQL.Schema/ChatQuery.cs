using System.Linq;
using GraphQL.Types;

namespace GraphQL.Samples.Schemas.Chat
{
    public class ChatQuery : ObjectGraphType
    {
        public ChatQuery(Links chat)
        {
            Field<ListGraphType<LinkType>>("links", resolve: context => chat.AllLinks.Take(100));
        }
    }
}
