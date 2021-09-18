using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Data.Doublets.Gql.Schema.Types
{
    /// <remarks>
    /// """
    /// unique or primary key constraints on table "links"
    /// """
    /// enum links_constraint {
    ///    """unique or primary key constraint"""
    ///    links_pkey
    /// }
    /// </remarks>
    class LinksConstraintEnumType : EnumerationGraphType<links_constraint>
    {
        public LinksConstraintEnumType() => Name = "LinksConstraintEnum";
    }
}

