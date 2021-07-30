using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Platform.Data.Doublets;

namespace GraphQL.Samples.Schemas.Link
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


        public Links(ILinks<ulong> Storage)
        {
            List<Link> links = new() { };
            var query = new Link<UInt64>(index: Storage.Constants.Any, source: Storage.Constants.Any, target: Storage.Constants.Any);
            Storage.Each(link =>
            {
                links.Add(new Link() { id = (long)Storage.GetIndex(link), from_id = (long)Storage.GetSource(link), to_id = (long)Storage.GetTarget(link) });
                return Storage.Constants.Continue;
            }, query);
            AllLinks = new ConcurrentStack<Link>();
            foreach (var link in links)
            {
                AllLinks.Push(new Link(){id = link.id, from_id = link.from_id, to_id = link.to_id});
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
            return null;
        }

        public void AddError(Exception exception) => _messageStream.OnError(exception);
    }
}
