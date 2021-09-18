using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    /// <remarks>
    /// """
    /// input type for inserting array relation for remote table "links"
    /// """
    /// input links_arr_rel_insert_input {
    ///   data: [links_insert_input!]!
    ///   on_conflict: links_on_conflict
    /// }
    /// </remarks>
    class LinksArrRelInsertInputType : InputObjectGraphType<LinksArrRelInsert>
    {
        public LinksArrRelInsertInputType()
        {
            Field<ListGraphType<LinksInsertInputType>>("data");
            Field(x => x.on_conflict, nullable: true, type: typeof(LinksOnConflictInputType));
        }
    }
}