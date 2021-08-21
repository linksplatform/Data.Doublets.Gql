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
        Link InsertLink(object service, Link link);
    }

    public class Links : ILinks
    {
        private readonly ISubject<Links> _messageStream = new ReplaySubject<Links>(1);

        public Links(ILinks<ulong> links)
        { }

        public Link InsertLink(object service, Link link)
        {
            ILinks<ulong> Links = (ILinks<ulong>) service;
            var create = Links.GetOrCreate((ulong) link.from_id, (ulong) link.to_id);
            return LinkType.GetLinkOrDefault(service, (long) create);
        }

        public void AddError(Exception exception) => _messageStream.OnError(exception);
    }
}
