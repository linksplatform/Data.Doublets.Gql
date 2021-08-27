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
            Field<LinksObjRelInsertInputType>("from");
            Field<LongGraphType>("from_id");
            Field<LongGraphType>("id");
            Field<LinksArrRelInsertInputType>("in");
            Field<LinksArrRelInsertInputType>("out");
            Field<LinksObjRelInsertInputType>("to");
            Field<LinksObjRelInsertInputType>("type");
            Field<LongGraphType>("to_id");
            Field<LongGraphType>("type_id");
        }
    }
}
