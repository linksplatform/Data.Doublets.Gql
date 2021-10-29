namespace Platform.Data.Doublets.Gql.Schema
{
    public class StringInsert
    {
#nullable enable
        public long? id { get; set; }
        public LinksArrayRelationshipInsert? link { get; set; }
        public long? link_id { get; set; }
        public string value { get; set; }
    }
}
