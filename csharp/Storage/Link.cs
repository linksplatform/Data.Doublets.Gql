using Platform.Data.Doublets;
using System;

namespace LinksStorage
{
    public class Link
    {
        public long Id { get; set; }

        public Link from { get; set; }

        public long from_id { get; set; }

        public Link to { get; set; }

        public long to_id { get; set; }

        public Link type {get;set;}

        public long type_id { get; set; }
    }
}
