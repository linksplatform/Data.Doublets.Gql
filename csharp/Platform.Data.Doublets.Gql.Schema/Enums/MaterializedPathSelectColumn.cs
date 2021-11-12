namespace Platform.Data.Doublets.Gql.Schema
{
    /// <remarks>
    ///     """
    ///     select columns of table "mp"
    ///     """
    ///     enum mp_select_column {
    ///     """column name"""
    ///     group_id
    ///     """column name"""
    ///     id
    ///     """column name"""
    ///     insert_category
    ///     """column name"""
    ///     item_id
    ///     """column name"""
    ///     path_item_depth
    ///     """column name"""
    ///     path_item_id
    ///     """column name"""
    ///     position_id
    ///     """column name"""
    ///     root_id
    ///     }
    /// </remarks>
    public enum MaterializedPathSelectColumn
    {
        group_id,
        id,
        insert_category,
        item_id,
        path_item_depth,
        path_item_id,
        position_id,
        root_id
    }
}
