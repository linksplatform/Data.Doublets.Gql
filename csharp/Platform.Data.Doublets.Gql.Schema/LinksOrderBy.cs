using Platform.Data.Doublets.Gql.Schema.Types.Input;

namespace Platform.Data.Doublets.Gql.Schema
{
    public class LinksOrderBy
    {
#nullable enable
        public LinksOrderBy? from { get; set; }
        public OrderBy? from_id { get; set; }
        public OrderBy? id { get; set; }
        public LinksAggregateOrderByInputType? in_aggregate { get; set; }
        public LinksAggregateOrderByInputType? out_aggregate { get; set; }
        public LinksOrderBy? to { get; set; }

        public OrderBy? to_id { get; set; }

        public LinksOrderBy? type { get; set; }

        public OrderBy? type_id { get; set; }
    }
}
