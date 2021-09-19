namespace Platform.Data.Doublets.Gql.Schema.Types.Enum
{
    /// <remarks>
    /// """
    /// select columns of table "links"
    /// """
    /// enum links_select_column {
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
    public class LinksSelectColumnEnumType : LinksColumnEnumType
    {
        public LinksSelectColumnEnumType() => Name = "links_select_column";
    }
}