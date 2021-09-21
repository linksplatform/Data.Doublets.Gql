namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    /// <remarks>
    ///     """
    ///     order by sum() on columns of table "links"
    ///     """
    ///     input links_sum_order_by {
    ///     from_id: OrderBy
    ///     id: OrderBy
    ///     to_id: OrderBy
    ///     type_id: OrderBy
    ///     }
    /// </remarks>
    public class LinksFieldsSumOrderByInputType : LinksFieldsOrderByInputType
    {
        public LinksFieldsSumOrderByInputType() : base("links_sum_order_by")
        {
        }
    }
}
