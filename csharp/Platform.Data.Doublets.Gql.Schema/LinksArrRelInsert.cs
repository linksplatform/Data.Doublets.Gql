using System.Collections.Generic;
using Platform.Data.Doublets.Gql.Schema.Types;

namespace Platform.Data.Doublets.Gql.Schema
{
    class LinksArrRelInsert
    {
        public List<LinksArrRelInsert> data { get; set; }


        public LinksOnConflict on_conflict { get; set; }
    }
}
