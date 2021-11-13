using System.Collections.Generic;
using Platform.Data.Doublets.Gql.Schema.Enums;

namespace Platform.Data.Doublets.Gql.Schema
{
    public class LinksOnConflict
    {
        public LinksConstraint constraint;

        public List<LinksColumn> update_columns;

        public LinksBooleanExpression where;
    }
}
