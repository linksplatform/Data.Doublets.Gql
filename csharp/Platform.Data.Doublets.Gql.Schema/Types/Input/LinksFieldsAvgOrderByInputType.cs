namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    /// <remarks>
    ///     """
    ///     order by avg() on columns of table "links"
    ///     """
    ///     input links_avg_order_by {
    ///     from_id: order_by
    ///     id: order_by
    ///     to_id: order_by
    ///     type_id: order_by
    ///     }
    /// </remarks>
    public class LinksFieldsAvgOrderByInputType : LinksFieldsOrderByInputType
    {
        public LinksFieldsAvgOrderByInputType() : base("links_avg_order_by")
        {
        }
    }
}
