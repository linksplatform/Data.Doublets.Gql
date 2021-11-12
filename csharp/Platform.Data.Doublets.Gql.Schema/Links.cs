using System.Collections.Generic;

namespace Platform.Data.Doublets.Gql.Schema
{
    public class Links
    {
        public Links(IList<ulong> links)
        {
            if (links is { Count: 3 })
            {
                var i = 0;
                id = (long)links[i++];
                from_id = (long)links[i++];
                to_id = (long)links[i];
            }
        }

        // public MPA? _by_root_aggregate { get; set; }
        public BooleanExpression bool_exp { get; set; }
        public Links? from { get; set; }
        public long? from_id { get; set; }
        public long id { get; set; }
        public List<Links> @in { get; set; }
        public LinksAggregate in_aggregate { get; set; }
        public Number number { get; set; }
        public List<Links> @out { get; set; }
        public LinksAggregate out_aggregate { get; set; }
        public string @string { get; set; }
        public Links to { get; set; }
        public long? to_id { get; set; }
        public Links type { get; set; }
        public long type_id { get; set; }
    }
}