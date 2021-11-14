using System.Collections.Generic;

namespace Platform.Data.Doublets.Gql.Schema
{
    public class LinksAggregate
    {
        public LinksAggregateFields aggregate { get; set; }
        public List<Links> nodes { get; set; }
    }
}