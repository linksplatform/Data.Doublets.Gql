using GraphQL.Types;

namespace GraphQL.Samples.Schemas.Chat
{
    public class ChatMutation : ObjectGraphType<object>
    {
        public ChatMutation(Chat chat)
        {
        }
    }

    public class MessageInputType : InputObjectGraphType
    {
        public MessageInputType()
        {
            Field<LongGraphType>("id");
            Field<MessageType>("from");
            Field<LongGraphType>("from_id");
            Field<MessageType>("to");
            Field<LongGraphType>("to_id");
            Field<MessageType>("type"); 
            Field<LongGraphType>("type_id");

        }
    }
}
