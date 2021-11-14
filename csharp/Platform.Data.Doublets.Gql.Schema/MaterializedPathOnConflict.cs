using Platform.Data.Doublets.Gql.Schema.Enums;
using System.Collections.Generic;

namespace Platform.Data.Doublets.Gql.Schema
{
    public class MaterializedPathOnConflict
    {
        public MaterializedPathConstraint constraint { get; set; }
        public List<MaterializedPathUpdateColumn> update_columns { get; set; }
        public MaterializedPathBooleanExpression where { get; set; }
    }
}
