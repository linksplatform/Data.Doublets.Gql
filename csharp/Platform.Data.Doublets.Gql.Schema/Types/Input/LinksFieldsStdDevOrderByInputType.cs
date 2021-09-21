namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    /// <remarks>
    ///     """
    ///     order by stddev() on columns of table "links"
    ///     """
    ///     input links_stddev_order_by {
    ///     from_id: OrderBy
    ///     id: OrderBy
    ///     to_id: OrderBy
    ///     type_id: OrderBy
    ///     }
    /// </remarks>
    public class LinksFieldsStdDevOrderByInputType : LinksFieldsOrderByInputType
    {
        public LinksFieldsStdDevOrderByInputType() : base("links_stddev_order_by")
        {
        }
    }
}