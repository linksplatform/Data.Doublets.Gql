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
    /// update columns of table "links"
    /// """
    /// enum links_update_column {
    ///   """column name"""
    ///   from_id
    ///
    ///   """column name"""
    ///   id
    ///
    ///   """column name"""
    ///   to_id
    ///
    ///   """column name"""
    ///   type_id
    /// }
    /// </remarks>
    class LinksUpdateColumnType : LinksColumnEnumType
    {
        public LinksUpdateColumnType() : base("links_update") {}
    }
}