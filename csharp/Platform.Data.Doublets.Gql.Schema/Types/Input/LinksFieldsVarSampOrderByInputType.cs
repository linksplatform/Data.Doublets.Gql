namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    /// <remarks>
    /// """
    /// order by var_samp() on columns of table "links"
    /// """
    /// input links_var_samp_order_by {
    ///   from_id: order_by
    ///   id: order_by
    ///   to_id: order_by
    ///   type_id: order_by
    /// }
    /// </remarks>
    public class LinksFieldsVarSampOrderByInputType : LinksFieldsOrderByInputType
    {
        public LinksFieldsVarSampOrderByInputType() : base("links_var_samp_order_by") {}
    }
}
