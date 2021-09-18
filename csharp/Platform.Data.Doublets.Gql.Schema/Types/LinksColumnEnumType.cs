using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema
{
    /// <remarks>
    /// """
    /// select columns of table "links"
    /// """
    /// enum links_select_column {
    ///    """column name"""
    ///    from_id
    ///
    ///    """column name"""
    ///    id
    ///
    ///    """column name"""
    ///    to_id
    ///
    ///    """column name"""
    ///    type_id
    /// }
    ///
    /// """
    /// update columns of table "links"
    /// """
    /// enum links_update_column {
    ///    """column name"""
    ///    from_id
    ///
    ///    """column name"""
    ///    id
    ///
    ///    """column name"""
    ///    to_id
    ///
    ///    """column name"""
    ///    type_id
    /// }
    /// </remarks>
    internal class LinksColumnType : EnumerationGraphType<LinksColumn>
    {
        public LinksColumnType() => Name = "LinksSelectColumnEnum";
    }
}
