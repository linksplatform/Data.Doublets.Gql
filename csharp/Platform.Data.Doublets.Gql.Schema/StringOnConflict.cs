using System.Collections.Generic;

namespace Platform.Data.Doublets.Gql.Schema
{
    public class StringOnConflict
    {
#nullable enable
        public StringConstraint constraint { get; set; }
        public List<StringUpdateColumn> update_columns { get; set; }
        public StringBooleanExpression? where { get; set; }
    }
}
