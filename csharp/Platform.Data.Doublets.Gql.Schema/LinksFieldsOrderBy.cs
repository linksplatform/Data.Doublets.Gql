using Platform.Data.Doublets.Gql.Schema.Enums;

namespace Platform.Data.Doublets.Gql.Schema
{
    public class LinksFieldsOrderBy
    {
        public OrderBy? id { get; set; }

        public OrderBy? from_id { get; set; }

        public OrderBy? to_id { get; set; }

        public OrderBy? type_id { get; set; }
    }
}
