using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Input;
using Platform.Data;
using Platform.Data.Doublets;

namespace Platform.Data.Doublets.Gql.Schema
{
    public interface ILinks
    {
       // ConcurrentStack<Link> AllLinks { get; }
        Link InsertLink(object service, Link link);

        IObservable<Link> Link(string user);
    }

    public class Links : ILinks
    {
        private readonly ISubject<Links> _messageStream = new ReplaySubject<Links>(1);

        private readonly ILinks<ulong> LinksSrorage;

        public Links(ILinks<ulong> links)
        {
            this.LinksSrorage = links;
            //List<Link> alllinks = new() { };
            //var query = new Link<UInt64>(index: links.Constants.Any, source: links.Constants.Any, target: links.Constants.Any);
            //AllLinks = new ConcurrentStack<Link>();
            //links.Each(link =>
            //{
            //    AllLinks.Push(new Link(link));
            //    return links.Constants.Continue;
            //, query);
        }

        public ConcurrentDictionary<string, string> Users { get; set; }

        //public ConcurrentStack<Link> AllLinks { get; set; }

        public Link InsertLink(object service, Link link)
        {
            ILinks<ulong> Links = (ILinks<ulong>) service;
            var create = Links.GetOrCreate((ulong) link.from_id, (ulong) link.to_id);
            return LinkType.GetLinkOrDefault(service, (long) create);
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
