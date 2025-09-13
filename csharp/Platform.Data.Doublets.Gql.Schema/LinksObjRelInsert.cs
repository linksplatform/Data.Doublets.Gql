namespace Platform.Data.Doublets.Gql.Schema
{
    public class LinksObjRelInsert
    {
        public LinksInsert data { get; set; }

        public LinksOnConflict on_conflict { get; set; }
    }
}
