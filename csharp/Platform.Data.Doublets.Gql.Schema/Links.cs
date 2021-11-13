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

        public BooleanExpression bool_exp;
        public Links? from;
        public long? from_id;
        public long id;
        public List<Links> @in;
        public LinksAggregate in_aggregate;
        public Number number;
        public List<Links> @out;
        public LinksAggregate out_aggregate;
        public string @string;
        public Links to;
        public long? to_id;
        public Links type;
        public long type_id;
    }
}
