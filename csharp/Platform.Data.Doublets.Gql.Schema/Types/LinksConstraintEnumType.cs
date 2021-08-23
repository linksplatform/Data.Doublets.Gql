using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gql.Samples.Schemas.Link.Types
{
    class LinksConstraintEnumType : EnumerationGraphType<Platform.Data.Doublets.Gql.Schema.links_constraint>
    {
        public LinksConstraintEnumType() => Name = "LinksConstraintEnum";
    }
}

