namespace Platform.Data.Doublets.Gql.Schema
{
    /// <remarks>
    ///     """
    ///     update columns of table "string"
    ///     """
    ///     enum string_update_column {
    ///     """column name"""
    ///     id
    ///     """column name"""
    ///     link_id
    ///     """column name"""
    ///     value
    ///     }
    /// </remarks>
    public enum StringUpdateColumn
    {
        id,
        link_id,
        value
    }
}