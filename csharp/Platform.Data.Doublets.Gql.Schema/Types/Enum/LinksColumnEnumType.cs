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
    ///
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
    public class LinksColumnEnumType : BaseEnumType<LinksColumn>
    {
        public LinksColumnEnumType(string name) : base(name)
        {

        }

        public LinksColumnEnumType(string name, string description) : base(name, description)
        {

        }
    }
}
