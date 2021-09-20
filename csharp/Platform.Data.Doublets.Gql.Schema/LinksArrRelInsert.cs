using System.Collections.Generic;

namespace Platform.Data.Doublets.Gql.Schema
{
public class LinksArrRelInsert
    {
        public List<LinksArrRelInsert> data { get; set; }


        public LinksOnConflict on_conflict { get; set; }
    }
}
