using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    class LinksOnConflictInputType : InputObjectGraphType<LinksConstraintEnumType>
    {
        public LinksOnConflictInputType()
        {
            Field<LinksConstraintEnumType>("constraint");
            Field<ListGraphType<LinksColumnType>>("update_columns");
            Field<LinkBooleanExpressionInputType>("where", null, nullable: true , null);
        }
    }
}
