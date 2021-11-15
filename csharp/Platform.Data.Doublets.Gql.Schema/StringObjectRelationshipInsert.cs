namespace Platform.Data.Doublets.Gql.Schema
{
    public class StringObjectRelationshipInsert
    {
        public StringInsert data { get; set; }

        public StringOnConflict on_conflict { get; set; }
    }
}
