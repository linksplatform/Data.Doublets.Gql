using Platform.Data.Doublets.Gql.Schema.Enums;

namespace Platform.Data.Doublets.Gql.Schema
{
    public class LinksAggregateOrderBy
    {
        public LinksFieldsOrderBy avg { get; set; }

        public OrderBy? count { get; set; }

        public LinksFieldsOrderBy max { get; set; }

        public LinksFieldsOrderBy min { get; set; }

        public LinksFieldsOrderBy stddev { get; set; }

        public LinksFieldsOrderBy stddev_pop { get; set; }

        public LinksFieldsOrderBy stddev_samp { get; set; }

        public LinksFieldsOrderBy sum { get; set; }

        public LinksFieldsOrderBy var_pop { get; set; }

        public LinksFieldsOrderBy var_samp { get; set; }

        public LinksFieldsOrderBy variance { get; set; }
    }
}