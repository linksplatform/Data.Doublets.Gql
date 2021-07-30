using Platform.Data.Doublets;
using System;

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
    }
}
