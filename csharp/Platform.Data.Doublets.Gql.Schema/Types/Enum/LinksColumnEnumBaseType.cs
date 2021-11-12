namespace Platform.Data.Doublets.Gql.Schema.Types.Enum
{
    /// <remarks>
    ///     """
    ///     update columns of table "links"
    ///     """
    ///     enum links_update_column {
    ///     """column name"""
    ///     from_id
    ///     """column name"""
    ///     id
    ///     """column name"""
    ///     to_id
    ///     """column name"""
    ///     type_id
    ///     }
    ///     """
    ///     select columns of table "links"
    ///     """
    ///     enum LinksColumn {
    ///     """column name"""
    ///     from_id
    ///     """column name"""
    ///     id
    ///     """column name"""
    ///     to_id
    ///     """column name"""
    ///     type_id
    ///     }
    /// </remarks>
    public class LinksColumnEnumBaseType : BaseEnumType<LinksColumn>
    {
        public LinksColumnEnumBaseType() : base(default)
        {
        }

        public LinksColumnEnumBaseType(string name) : base(name)
        {
        }

        public LinksColumnEnumBaseType(string name, string description) : base(name, description)
        {
        }
    }
}
