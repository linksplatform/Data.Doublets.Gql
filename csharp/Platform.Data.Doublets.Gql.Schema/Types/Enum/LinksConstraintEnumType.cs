﻿using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Enum
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
    public class LinksConstraintEnumType : EnumerationGraphType<links_constraint>
    {
        protected override string ChangeEnumCase(string value)
        {
            return value.ToSnakeCase();
        }
        public LinksConstraintEnumType()
        {
            Name = "links_constraint";
        }
    }
}