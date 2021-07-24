using GraphQL.Types;

namespace GraphQL.Samples.Schemas.Chat
{
    public class ChatMutation : ObjectGraphType<object>
    {
        public ChatMutation(Links chat)
        {
        }
    }

    public class MessageInputType : InputObjectGraphType
    {
        public MessageInputType()
        {
            Field<LongGraphType>("id");
            Field<LinkType>("from");
            Field<LongGraphType>("from_id");
            Field<LinkType>("to");
            Field<LongGraphType>("to_id");
            Field<LinkType>("type"); 
            Field<LongGraphType>("type_id");
        }
    }
}
