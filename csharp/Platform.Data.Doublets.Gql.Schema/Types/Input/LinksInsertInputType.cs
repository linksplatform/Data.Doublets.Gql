using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    class LinksInsertInputType : InputObjectGraphType
    {
        public LinksInsertInputType()
        {
            Field<LinksObjRelInsertInputType>("from", null, true, null);
            Field<LongGraphType>("from_id", null, true, null);
            Field<LongGraphType>("id", null, true, null);
            Field<LinksArrRelInsertInputType>("in", null, true, null);
            Field<LinksArrRelInsertInputType>("out", null, true, null);
            Field<LinksObjRelInsertInputType>("to", null, true, null);
            Field<LinksObjRelInsertInputType>("type", null, true, null);
            Field<LongGraphType>("to_id", null, true, null);
            Field<LongGraphType>("type_id", null, true, null);
        }
    }
}
