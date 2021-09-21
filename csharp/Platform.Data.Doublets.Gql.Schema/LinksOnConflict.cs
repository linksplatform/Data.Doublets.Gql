using System.Collections.Generic;

namespace Platform.Data.Doublets.Gql.Schema
{
    public class LinksOnConflict
    {
        public links_constraint constraint { get; set; }

        public List<LinksColumn> update_columns { get; set; }

        public LinksBooleanExpression where { get; set; }
    }
}