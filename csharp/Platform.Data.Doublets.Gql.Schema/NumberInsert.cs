namespace Platform.Data.Doublets.Gql.Schema
{
    public class NumberInsert
    {
        public long? id { get; set; }
        public LinksArrRelInsert? link { get; set; }
        public long? link_id { get; set; }
        public float? value { get; set; }
    }
}
