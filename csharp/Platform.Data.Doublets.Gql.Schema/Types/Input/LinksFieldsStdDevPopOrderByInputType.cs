namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    /// <remarks>
    ///     """
    ///     order by stddev_pop() on columns of table "links"
    ///     """
    ///     input links_stddev_pop_order_by {
    ///     from_id: OrderBy
    ///     id: OrderBy
    ///     to_id: OrderBy
    ///     type_id: OrderBy
    ///     }
    /// </remarks>
    public class LinksFieldsStdDevPopOrderByInputType : LinksFieldsOrderByInputType
    {
        public LinksFieldsStdDevPopOrderByInputType() : base("links_stddev_pop_order_by")
        {
        }
    }
}