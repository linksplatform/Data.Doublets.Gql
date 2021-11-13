using System.Collections.Generic;
using Platform.Data.Doublets.Gql.Schema.Enums;

namespace Platform.Data.Doublets.Gql.Schema
{
    public class NumberOnConflict
    {
        public NumberConstraint constraint;
        public List<NumberUpdateColumn> update_columns;
        public NumberBooleanExpression where;
    }
}
