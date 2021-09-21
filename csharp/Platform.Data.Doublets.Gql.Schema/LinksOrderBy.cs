namespace Platform.Data.Doublets.Gql.Schema
{
    public class LinksOrderBy
    {
        public LinksOrderBy from { get; set; }

        public order_by? from_id { get; set; }

        public order_by? id { get; set; }

        public LinksOrderBy to { get; set; }

        public order_by? to_id { get; set; }

        public LinksOrderBy type { get; set; }

        public order_by? type_id { get; set; }
    }
}