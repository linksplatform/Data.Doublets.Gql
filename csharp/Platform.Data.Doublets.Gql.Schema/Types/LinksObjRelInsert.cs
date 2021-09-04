using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Data.Doublets.Gql.Schema.Types
{
    class LinksObjRelInsert
    {
        public LinksObjRelInsert data { get; set; }


        public LinksOnConflict? on_conflict { get; set; }
    }
}
