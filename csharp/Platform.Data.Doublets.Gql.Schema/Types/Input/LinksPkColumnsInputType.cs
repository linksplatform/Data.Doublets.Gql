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
    /// primary key columns input for table: "links"
    /// """
    /// input links_pk_columns_input {
    ///   id: bigint!
    /// }
    /// </remarks>
public class LinksPkColumnsInputType : InputObjectGraphType
    {
        public LinksPkColumnsInputType()
        {
            Name = "links_pk_columns_input";
            Field<LongGraphType>("id");
        }
    }
}
