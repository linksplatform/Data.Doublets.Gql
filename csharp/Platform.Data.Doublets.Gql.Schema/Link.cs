using GraphQL.Types;
using Platform.Data.Doublets;
using System;
using System.Collections.Generic;

namespace Platform.Data.Doublets.Gql.Schema
{
    public class Link
    {
        public long id { get; set; }

        public Link from { get; set; }

        public long from_id { get; set; }

        public Link to { get; set; }

        public long to_id { get; set; }

        public Link type {get;set;}

        public long type_id { get; set; }
        public Link(IList<UInt64> links)
        {
            var i = 0;
            id = (long)links[i++];
            from_id = (long)links[i++];
            to_id = (long)links[i++];
        }
    }
}
