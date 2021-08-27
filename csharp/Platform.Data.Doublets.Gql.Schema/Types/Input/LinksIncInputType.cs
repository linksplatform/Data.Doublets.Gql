using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    class LinksIncInputType : InputObjectGraphType
    {
        public LinksIncInputType()
        {
            Field<LongGraphType>("id", null, nullable: true, null);
            Field<LongGraphType>("from_id", null, nullable: true, null);
            Field<LongGraphType>("to_id", null, nullable: true, null);
            Field<LongGraphType>("type_id", null, nullable: true, null);
        }
    }
}
