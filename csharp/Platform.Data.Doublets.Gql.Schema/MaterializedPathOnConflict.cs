using System.Collections.Generic;

namespace Platform.Data.Doublets.Gql.Schema
{
    public class MaterializedPathOnConflict
    {
        public MaterializedPathConstraint constraint;
        public List<MaterializedPathUpdateColumn> update_columns;
        public MaterializedPathBooleanExpression where;
    }
}
