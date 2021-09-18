using Platform.Data.Doublets.Gql.Schema.Types;

namespace Platform.Data.Doublets.Gql.Schema
{
    class LinksObjRelInsert
    {
        public LinksObjRelInsert data { get; set; }

        public LinksOnConflict on_conflict { get; set; }
    }
}
