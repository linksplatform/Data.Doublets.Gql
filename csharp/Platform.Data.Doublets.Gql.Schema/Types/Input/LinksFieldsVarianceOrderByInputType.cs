namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    /// <remarks>
    ///     """
    ///     order by variance() on columns of table "links"
    ///     """
    ///     input links_variance_order_by {
    ///     from_id: OrderBy
    ///     id: OrderBy
    ///     to_id: OrderBy
    ///     type_id: OrderBy
    ///     }
    /// </remarks>
    public class LinksFieldsVarianceOrderByInputType : LinksFieldsOrderByInputType
    {
        public LinksFieldsVarianceOrderByInputType() : base("links_variance_order_by")
        {
        }
    }
}
