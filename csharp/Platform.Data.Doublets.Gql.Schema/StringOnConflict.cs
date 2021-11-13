using System.Collections.Generic;

namespace Platform.Data.Doublets.Gql.Schema
{
    public class StringOnConflict
    {
        public StringConstraint constraint;
        public List<StringUpdateColumn> update_columns;
        public StringBooleanExpression where;
    }
}
