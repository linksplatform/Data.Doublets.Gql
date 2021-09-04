using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    class LinksOnConflictInputType : InputObjectGraphType<LinksOnConflict>
    {
        public LinksOnConflictInputType()
        {
            Field<LinksConstraintEnumType>("constraint");
            Field<ListGraphType<LinksColumnType>>("update_columns");
            Field(x => x.where, nullable: true, type: typeof(LinkBooleanExpressionInputType));
        }
    }
}
