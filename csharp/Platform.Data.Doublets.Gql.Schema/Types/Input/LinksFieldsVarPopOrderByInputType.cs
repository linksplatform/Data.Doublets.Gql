namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    /// <remarks>
    ///     """
    ///     order by var_pop() on columns of table "links"
    ///     """
    ///     input links_var_pop_order_by {
    ///     from_id: OrderBy
    ///     id: OrderBy
    ///     to_id: OrderBy
    ///     type_id: OrderBy
    ///     }
    /// </remarks>
    public class LinksFieldsVarPopOrderByInputType : LinksFieldsOrderByInputType
    {
        public LinksFieldsVarPopOrderByInputType() : base("links_var_pop_order_by")
        {
        }
    }
}
