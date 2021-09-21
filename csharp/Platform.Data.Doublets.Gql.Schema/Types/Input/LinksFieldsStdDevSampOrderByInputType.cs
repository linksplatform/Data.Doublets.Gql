namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    /// <remarks>
    ///     """
    ///     order by stddev_samp() on columns of table "links"
    ///     """
    ///     input links_stddev_samp_order_by {
    ///     from_id: OrderBy
    ///     id: OrderBy
    ///     to_id: OrderBy
    ///     type_id: OrderBy
    ///     }
    /// </remarks>
    public class LinksFieldsStdDevSampOrderByInputType : LinksFieldsOrderByInputType
    {
        public LinksFieldsStdDevSampOrderByInputType() : base("links_stddev_samp_order_by")
        {
        }
    }
}
