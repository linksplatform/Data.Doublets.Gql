using GraphQL.Types;
using Platform.Data.Doublets.Gql.Schema.Types.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    class LinksObjRelInsertInputType : InputObjectGraphType
    {
        public LinksObjRelInsertInputType()
        {
            Field<LinksInsertInputType>("data");
            Field<LinksOnConflictInputType>("on_conflict");
        }
    }
}
