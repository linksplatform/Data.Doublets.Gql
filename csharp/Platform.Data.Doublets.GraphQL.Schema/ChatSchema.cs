using System;
using GraphQL.Types;

namespace GraphQL.Samples.Schemas.Chat
{
    public class ChatSchema : Schema
    {
        public ChatSchema(Links chat, IServiceProvider provider) : base(provider)
        {
            Query = new ChatQuery(chat);
            //Mutation = new ChatMutation(chat);
            //Subscription = new ChatSubscriptions();
        }
    }
}
