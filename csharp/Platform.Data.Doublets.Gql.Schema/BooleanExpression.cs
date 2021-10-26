using System.Collections.Generic;

namespace Platform.Data.Doublets.Gql.Schema
{
    public class BooleanExpression
    {
        public string? gql { get; set; }
        public long id { get; set; }
        public List<Links> link { get; set; }
        public LinksAggregate link_aggregate { get; set; }
        public long? link_id { get; set; }
        public string? sql { get; set; }
    }
}
