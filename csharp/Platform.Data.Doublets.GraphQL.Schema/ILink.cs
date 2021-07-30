using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Platform.Data;
using Platform.Data.Doublets;

namespace GraphQL.Samples.Schemas.Link
{
    public interface ILinks
    {
        ConcurrentStack<Link> AllLinks { get; }
        Link insert_link(object service, Link link);

        //Message AddLink(Message message);

        IObservable<Link> Link(string user);

        //Message AddMessage(ReceivedMessage message);
    }

    public class Links : ILinks
    {
        private readonly ISubject<Links> _messageStream = new ReplaySubject<Links>(1);

        private readonly ILinks<ulong> LinksSrorage;

        public Links(ILinks<ulong> links)
        {
            this.LinksSrorage = links;
            List<Link> alllinks = new() { };
            var query = new Link<UInt64>(index: links.Constants.Any, source: links.Constants.Any, target: links.Constants.Any);
            links.Each(link =>
            {
                alllinks.Add(new Link() { id = (long)links.GetIndex(link), from_id = (long)links.GetSource(link), to_id = (long)links.GetTarget(link) });
                return links.Constants.Continue;
            }, query);
            AllLinks = new ConcurrentStack<Link>();
            foreach (var link in alllinks)
            {
                AllLinks.Push(new Link(){id = link.id, from_id = link.from_id, to_id = link.to_id});
            }
        }

        public ConcurrentDictionary<string, string> Users { get; set; }

        public ConcurrentStack<Link> AllLinks { get; set; }

        public Link insert_link(object service, Link link)
        {
            //var service = context.RequestServices.GetService(typeof(ILinks<ulong>));
            ILinks<ulong> Links = (ILinks<ulong>)service;
            if (Links.Exists((ulong)link.from_id))
            {
                //var fromLink = Links.GetLink((ulong)link.from_id);
                return new Link()
                {
                    id = link.id,
                    from_id = link.from_id,
                    to_id = link.to_id
                };
            }
            else
            {
                return null;
            }            //return (long)LinksSrorage.GetOrCreate(source: (ulong)link.from_id, target: (ulong)link.to_id);
        }

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
