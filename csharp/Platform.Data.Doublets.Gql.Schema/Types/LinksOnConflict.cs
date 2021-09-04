using Platform.Data.Doublets.Gql.Schema.Types.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Data.Doublets.Gql.Schema.Types
{
    public class LinksOnConflict
    {
        public links_constraint constraint { get; set; }

        public List<LinksColumn> update_columns { get; set; }

        public LinkBooleanExpression where { get; set; }
    }
}
