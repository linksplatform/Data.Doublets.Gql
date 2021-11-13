using System.Collections.Generic;
using Platform.Data.Doublets.Gql.Schema.Enums;

namespace Platform.Data.Doublets.Gql.Schema
{
    public class MaterializedPathOnConflict
    {
        public MaterializedPathConstraint constraint;
        public List<MaterializedPathUpdateColumn> update_columns;
        public MaterializedPathBooleanExpression where;
    }
}
