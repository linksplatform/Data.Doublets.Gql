namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    /// <remarks>
    /// """
    /// order by stddev_pop() on columns of table "links"
    /// """
    /// input links_stddev_pop_order_by {
    ///   from_id: order_by
    ///   id: order_by
    ///   to_id: order_by
    ///   type_id: order_by
    /// }
    /// </remarks>
    public class LinksFieldsStdDevPopOrderByInputType : LinksFieldsOrderByInputType
    {
        public LinksFieldsStdDevPopOrderByInputType() : base("links_stddev_pop_order_by") {}
    }
}
