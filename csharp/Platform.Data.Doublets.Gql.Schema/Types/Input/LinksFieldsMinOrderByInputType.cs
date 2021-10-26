namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    /// <remarks>
    ///     """
    ///     order by min() on columns of table "links"
    ///     """
    ///     input links_min_order_by {
    ///     from_id: order_by
    ///     id: order_by
    ///     to_id: order_by
    ///     type_id: order_by
    ///     }
    /// </remarks>
    public class LinksFieldsMinOrderByInputType : LinksFieldsOrderByInputType
    {
        public LinksFieldsMinOrderByInputType() : base("links_min_order_by")
        {
        }
    }
}
