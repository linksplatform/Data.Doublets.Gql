using LinksStorage;
using System;
using System.Collections.Concurrent;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace GraphQL.Samples.Schemas.Chat
{
    public interface ILinks
    {
        ConcurrentStack<Link> AllLinks { get; }

       // Message AddMessage(Message message);

        IObservable<Link> Link(string user);

        //Message AddMessage(ReceivedMessage message);
    }

    public class Links : ILinks
    {
        private readonly ISubject<Links> _messageStream = new ReplaySubject<Links>(1);

        public Links()
        {
            var links = new Storage("db.links").GetAllLinks();
            AllLinks = new ConcurrentStack<Link>();
            foreach (var link in links)
            {
                AllLinks.Push(new Link(){Id = link.Id, from_id = link.from_id, to_id = link.to_id});
            }
        }

        public ConcurrentDictionary<string, string> Users { get; set; }

        public ConcurrentStack<Link> AllLinks { get; set; }

        //public Message AddMessage(Message message)
        //{
        //    AllMessages.Push(message);
        //    _messageStream.OnNext(message);
        //    return message;
        //}

        public IObservable<Link> Link(string user)
        {
            //return _messageStream
            //    .Select(message =>
            //    {
            //        return message;
            //    })
            var a = new Storage("db.links").GetAllLinks().ToObservable();
            return a;
        }

        public void AddError(Exception exception) => _messageStream.OnError(exception);
    }
}
