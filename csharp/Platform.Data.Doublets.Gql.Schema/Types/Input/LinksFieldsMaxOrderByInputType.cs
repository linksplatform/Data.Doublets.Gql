namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    /// <remarks>
    ///     """
    ///     order by max() on columns of table "links"
    ///     """
    ///     input links_max_order_by {
    ///     from_id: OrderBy
    ///     id: OrderBy
    ///     to_id: OrderBy
    ///     type_id: OrderBy
    ///     }
    /// </remarks>
    public class LinksFieldsMaxOrderByInputType : LinksFieldsOrderByInputType
    {
        public LinksFieldsMaxOrderByInputType() : base("links_max_order_by")
        {
        }
    }
}