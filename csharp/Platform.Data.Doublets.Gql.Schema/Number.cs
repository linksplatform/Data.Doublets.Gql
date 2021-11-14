using System.Collections.Generic;

namespace Platform.Data.Doublets.Gql.Schema
{
    public class Number
    {
        public long id;
        public List<Links> link;
        public LinksAggregate link_aggregate;
        public long? link_id;
        public double? value;
    }
}