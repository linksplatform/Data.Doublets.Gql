using System.Collections.Generic;

namespace Platform.Data.Doublets.Gql.Schema
{
    public class Links
    {
        public Links()
        {
        }

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

        public long id { get; set; }

        public Links from { get; set; }

        public long from_id { get; set; }

        public Links to { get; set; }

        public long to_id { get; set; }

        public Links type { get; set; }

        public long type_id { get; set; }
    }
}