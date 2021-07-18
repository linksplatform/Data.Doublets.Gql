using Platform.Data.Doublets;
using System;

namespace GraphQL.Samples.Schemas.Chat
{
    public class Message
    {
        public long Id { get; set; }

        public Link<Message> from { get; set; }

        public long from_id { get; set; }

        public Link<Message> to { get; set; }

        public long to_id { get; set; }

        public Link<Message> type {get;set;}

        public long type_id { get; set; }
    }
}
