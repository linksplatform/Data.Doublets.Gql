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
    }

    public class Links : ILinks
    {
        private readonly ISubject<Links> _messageStream = new ReplaySubject<Links>(1);

        public void AddError(Exception exception) => _messageStream.OnError(exception);
    }
}
