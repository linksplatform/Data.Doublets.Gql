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
            Field<LinksObjRelInsertInputType>("from", null, nullable: true, null);
            Field<LongGraphType>("from_id", null, nullable: true, null);
            Field<LongGraphType>("id", null, nullable: true, null);
            Field<LinksArrRelInsertInputType>("in", null, nullable: true, null);
            Field<LinksArrRelInsertInputType>("out", null, nullable: true, null);
            Field<LinksObjRelInsertInputType>("to", null, nullable: true, null);
            Field<LinksObjRelInsertInputType>("type", null, nullable: true, null);
            Field<LongGraphType>("to_id", null, nullable: true, null);
            Field<LongGraphType>("type_id", null, nullable: true, null);
        }
    }
}
