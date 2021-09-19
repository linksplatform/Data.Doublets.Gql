namespace Platform.Data.Doublets.Gql.Schema
{
    public class LinksInsert
    {
        public LinksObjRelInsert from { get; set; }

        public long? from_id { get; set; }

        public long? id { get; set; }

        public long? to_id { get; set; }

        public long? type_id { get; set; }

        public LinksArrRelInsert @in { get; set; }

        public LinksArrRelInsert @out { get; set; }

        public LinksObjRelInsert type { get; set; }

        public LinksObjRelInsert to { get; set; }
    }
}
