namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    /// <remarks>
    ///     """
    ///     order by stddev_samp() on columns of table "links"
    ///     """
    ///     input links_stddev_samp_order_by {
    ///     from_id: order_by
    ///     id: order_by
    ///     to_id: order_by
    ///     type_id: order_by
    ///     }
    /// </remarks>
    public class LinksFieldsStdDevSampOrderByInputType : LinksFieldsOrderByInputType
    {
        public LinksFieldsStdDevSampOrderByInputType() : base("links_stddev_samp_order_by")
        {
        }
    }
}