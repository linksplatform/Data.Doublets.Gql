using System.Collections.Generic;

namespace Platform.Data.Doublets.Gql.Schema
{
    public class NumberOnConflict
    {
        public NumberConstraint constraint { get; set; }
        public List<NumberUpdateColumn> update_columns { get; set; }
        public NumberBooleanExpression where { get; set; }
    }
}