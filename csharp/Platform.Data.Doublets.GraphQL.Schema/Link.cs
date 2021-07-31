using GraphQL.Types;
using Platform.Data.Doublets;
using System;
using System.Collections.Generic;

namespace GraphQL.Samples.Schemas.Link
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

        public ListGraphType<LinkType> In { get; set; }

        public ListGraphType<LinkType> Out { get; set; }
    }
}
