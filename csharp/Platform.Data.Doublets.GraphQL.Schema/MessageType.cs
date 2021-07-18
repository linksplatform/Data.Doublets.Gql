using GraphQL.Types;

namespace GraphQL.Samples.Schemas.Chat
{
    public class MessageType : ObjectGraphType<Message>
    {
        public MessageType()
        {
            Field(o => o.Id);
            Field(o => o.from_id);
            Field(o => o.from, type: typeof(MessageType));
            Field(o => o.to, type: typeof(MessageType));
            Field(o => o.to_id);
            Field(o => o.type, type: typeof(MessageType));
            Field(o => o.type_id);
        }

        //private MessageFrom ResolveFrom(IResolveFieldContext<Message> context)
        //{
        //    var message = context.Source;
        //    return message.From;
        //}
    }
}
