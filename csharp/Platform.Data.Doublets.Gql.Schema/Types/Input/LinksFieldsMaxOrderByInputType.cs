namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    /// <remarks>
    ///     """
    ///     order by max() on columns of table "links"
    ///     """
    ///     input links_max_order_by {
    ///     from_id: order_by
    ///     id: order_by
    ///     to_id: order_by
    ///     type_id: order_by
    ///     }
    /// </remarks>
    public class LinksFieldsMaxOrderByInputType : LinksFieldsOrderByInputType
    {
        public LinksFieldsMaxOrderByInputType() : base("links_max_order_by")
        {
        }
    }
}
