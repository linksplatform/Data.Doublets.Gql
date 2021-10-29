using System.Collections.Generic;

namespace Platform.Data.Doublets.Gql.Schema
{
    public class MaterializedPathArrayRelationshipInsert
    {
#nullable enable
        public List<MaterializedPathArrayRelationshipInsert> data { get; set; }
        public MaterializedPathOnConflict? on_conflict { get; set; }
    }
}
