namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    /// <remarks>
    ///     """
    ///     order by var_pop() on columns of table "links"
    ///     """
    ///     input links_var_pop_order_by {
    ///     from_id: order_by
    ///     id: order_by
    ///     to_id: order_by
    ///     type_id: order_by
    ///     }
    /// </remarks>
    public class LinksFieldsVarPopOrderByInputType : LinksFieldsOrderByInputType
    {
        public LinksFieldsVarPopOrderByInputType() : base("links_var_pop_order_by")
        {
        }
    }
}
