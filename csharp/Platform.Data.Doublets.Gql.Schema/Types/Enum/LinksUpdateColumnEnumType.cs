namespace Platform.Data.Doublets.Gql.Schema.Types.Enum
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
    class LinksUpdateColumnEnumType : LinksColumnEnumType
    {
        public LinksUpdateColumnEnumType() : base("links_update_column") { }
    }
}