namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    /// <remarks>
    ///     """
    ///     order by sum() on columns of table "links"
    ///     """
    ///     input links_sum_order_by {
    ///     from_id: order_by
    ///     id: order_by
    ///     to_id: order_by
    ///     type_id: order_by
    ///     }
    /// </remarks>
    public class LinksFieldsSumOrderByInputType : LinksFieldsOrderByInputType
    {
        public LinksFieldsSumOrderByInputType() : base("links_sum_order_by")
        {
        }
    }
}
