using GraphQL.Types;
using Platform.Data.Doublets.Gql.Schema.Enums;

namespace Platform.Data.Doublets.Gql.Schema.Types
{
    /// <remarks>
    ///     """
    ///     unique or primary key constraints on table "links"
    ///     """
    ///     enum links_constraint {
    ///     """unique or primary key constraint"""
    ///     links_pkey
    ///     }
    /// </remarks>
    internal class LinksConstraintEnumType : EnumerationGraphType<LinksConstraint>
    {
        public LinksConstraintEnumType()
        {
            Name = "LinksConstraintEnum";
        }
    }
}
