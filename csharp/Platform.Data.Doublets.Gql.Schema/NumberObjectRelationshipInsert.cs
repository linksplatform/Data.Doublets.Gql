namespace Platform.Data.Doublets.Gql.Schema
{
    public class NumberObjectRelationshipInsert
    {
        public NumberInsert data { get; set; }
        public NumberOnConflict? on_conflict { get; set; }
    }
}
