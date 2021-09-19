using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Platform.Data.Doublets.Gql.Schema.Types.Enum;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    /// <remarks>
    /// """
    /// on conflict condition type for table "links"
    /// """
    /// input links_on_conflict {
    ///   constraint: links_constraint!
    ///   update_columns: [links_update_column!]!
    ///   where: links_bool_exp
    /// }
    /// </remarks>
    class LinksOnConflictInputType : InputObjectGraphType<LinksOnConflict>
    {
        public LinksOnConflictInputType()
        {
            Name = "links_on_conflict";
            Field<LinksConstraintEnumType>("constraint");
            Field<ListGraphType<LinksUpdateColumnEnumType>>("update_columns");
            Field(x => x.where, nullable: true, type: typeof(LinksBooleanExpressionInputType));
        }
    }
}
