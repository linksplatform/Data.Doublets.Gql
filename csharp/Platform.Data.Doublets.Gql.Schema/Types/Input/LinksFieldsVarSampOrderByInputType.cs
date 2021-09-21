namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    /// <remarks>
    ///     """
    ///     order by var_samp() on columns of table "links"
    ///     """
    ///     input links_var_samp_order_by {
    ///     from_id: OrderBy
    ///     id: OrderBy
    ///     to_id: OrderBy
    ///     type_id: OrderBy
    ///     }
    /// </remarks>
    public class LinksFieldsVarSampOrderByInputType : LinksFieldsOrderByInputType
    {
        public LinksFieldsVarSampOrderByInputType() : base("links_var_samp_order_by")
        {
        }
    }
}
