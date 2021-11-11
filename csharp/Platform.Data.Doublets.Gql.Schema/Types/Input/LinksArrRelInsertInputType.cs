using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    class LinksArrRelInsertInputType : InputObjectGraphType<LinksArrRelInsert>
    {
        public LinksArrRelInsertInputType()
        {
            Field<ListGraphType<LinksInsertInputType>>("data");
            Field(x => x.on_conflict, nullable: true, type: typeof(LinksOnConflictInputType));
        }
    }
}