using System.Collections.Generic;
using Platform.Data.Doublets.Gql.Schema.Enums;

namespace Platform.Data.Doublets.Gql.Schema
{
    public class LinksOnConflict
    {
        public LinksConstraint constraint { get; set; }

        public List<LinksColumn> update_columns { get; set; }

        public LinksBooleanExpression where { get; set; }
    }
}
