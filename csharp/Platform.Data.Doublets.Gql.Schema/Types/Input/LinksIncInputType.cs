using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    class LinksIncInputType : InputObjectGraphType<Link>
    {
        public LinksIncInputType()
        {
            Field(x => x.id, nullable: true, type: typeof(LongGraphType));
            Field(x => x.from_id, nullable: true, type: typeof(LongGraphType));
            Field(x => x.to_id, nullable: true, type: typeof(LongGraphType));
            Field(x => x.type_id, nullable: true, type: typeof(LongGraphType));
        }
    }
}
