using Platform.Data.Doublets.Gql.Schema.Types.Input;

namespace Platform.Data.Doublets.Gql.Schema
{
    public class LinksOrderBy
    {
        public LinksOrderBy from;
        public OrderBy? from_id;
        public OrderBy? id;
        public LinksAggregateOrderByInputType in_aggregate;
        public LinksAggregateOrderByInputType out_aggregate;
        public LinksOrderBy to;

        public OrderBy? to_id;

        public LinksOrderBy type;

        public OrderBy? type_id;
    }
}
