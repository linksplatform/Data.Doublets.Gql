using System.Collections.Generic;

namespace Platform.Data.Doublets.Gql.Schema
{
    public class MaterializedPathOnConflict
    {
#nullable enable
        public MaterializedPathConstraint constraint { get; set; }
        public List<MaterializedPathUpdateColumn> update_columns { get; set; }
        public MaterializedPathBooleanExpression? where { get; set; }
    }
}
