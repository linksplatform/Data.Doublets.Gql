using System.Collections.Generic;

namespace Platform.Data.Doublets.Gql.Schema
{
    public class NumberOnConflict
    {
        public NumberConstraint constraint;
        public List<NumberUpdateColumn> update_columns;
        public NumberBooleanExpression where;
    }
}
