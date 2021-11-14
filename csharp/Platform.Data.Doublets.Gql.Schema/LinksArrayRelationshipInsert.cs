using System.Collections.Generic;

namespace Platform.Data.Doublets.Gql.Schema
{
    public class LinksArrayRelationshipInsert
    {
        public List<LinksArrayRelationshipInsert> data { get; set; }
        public LinksOnConflict on_conflict { get; set; }
    }
}