namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    /// <remarks>
    ///     """
    ///     order by min() on columns of table "links"
    ///     """
    ///     input links_min_order_by {
    ///     from_id: OrderBy
    ///     id: OrderBy
    ///     to_id: OrderBy
    ///     type_id: OrderBy
    ///     }
    /// </remarks>
    public class LinksFieldsMinOrderByInputType : LinksFieldsOrderByInputType
    {
        public LinksFieldsMinOrderByInputType() : base("links_min_order_by")
        {
        }
    }
}