namespace Platform.Data.Doublets.Gql.Schema
{
    public class BooleanExpressionInsert
    {
        public string gql { get; set; }

        public long? id { get; set; }

        public LinksArrayRelationshipInsert link { get; set; }

        public long? link_id { get; set; }

        public string sql { get; set; }
    }
}
