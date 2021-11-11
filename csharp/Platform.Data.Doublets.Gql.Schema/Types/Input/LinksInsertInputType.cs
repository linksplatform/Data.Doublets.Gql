using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    class LinksInsertInputType : InputObjectGraphType<LinksInsert>
    {
        public LinksInsertInputType()
        {
            Field(x => x.from, nullable: true, type: typeof(LinksObjRelInsertInputType));
            Field(x => x.from_id, nullable: true, type: typeof(LongGraphType));
            Field(x => x.id, nullable: true, type: typeof(LongGraphType));
            Field(x => x.@in, nullable: true, type: typeof(LinksArrRelInsertInputType));
            Field(x => x.@out, nullable: true, type: typeof(LinksArrRelInsertInputType));
            Field(x => x.to, nullable: true, type: typeof(LinksObjRelInsertInputType));
            Field(x => x.type, nullable: true, type: typeof(LinksObjRelInsertInputType));
            Field(x => x.to_id, nullable: true, type: typeof(LongGraphType));
            Field(x => x.type_id, nullable: true, type: typeof(LongGraphType));
        }
    }
}
