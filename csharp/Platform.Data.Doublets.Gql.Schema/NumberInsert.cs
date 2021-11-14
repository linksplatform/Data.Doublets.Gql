namespace Platform.Data.Doublets.Gql.Schema
{
    public class NumberInsert
    {
        public long? id { get; set; }
        public LinksArrayRelationshipInsert link { get; set; }
        public long? link_id { get; set; }
        public double? value { get; set; }
    }
}