using System.Collections.Generic;

namespace Platform.Data.Doublets.Gql.Schema
{
    public class LinksOnConflict
    {
        public LinksConstraint constraint;

        public List<LinksColumn> update_columns;

        public LinksBooleanExpression where;
    }
}
