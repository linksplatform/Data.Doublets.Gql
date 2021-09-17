using GraphQL.Types;
using Platform.Data.Doublets.Gql.Schema.Types.Input;


namespace Platform.Data.Doublets.Gql.Schema.Types
{
    /// <remarks>
    /// """
    /// order by max() on columns of table "links"
    /// """
    /// input links_max_order_by {
    ///   from_id: order_by
    ///   id: order_by
    ///   to_id: order_by
    ///   type_id: order_by
    /// }
    ///
    /// """
    /// order by sum() on columns of table "links"
    /// """
    /// input links_sum_order_by {
    /// from_id: order_by
    ///     id: order_by
    ///     to_id: order_by
    ///     type_id: order_by
    /// }
    ///
    /// """
    /// order by min() on columns of table "links"
    /// """
    /// input links_min_order_by {
    ///   from_id: order_by
    ///   id: order_by
    ///   to_id: order_by
    ///   type_id: order_by
    /// }
    ///
    /// """
    /// order by variance() on columns of table "links"
    /// """
    /// input links_variance_order_by {
    /// from_id: order_by
    ///     id: order_by
    ///     to_id: order_by
    ///     type_id: order_by
    /// }
    ///
    /// """
    /// order by avg() on columns of table "links"
    /// """
    /// input links_avg_order_by {
    ///   from_id: order_by
    ///   id: order_by
    ///   to_id: order_by
    ///   type_id: order_by
    /// }
    /// 
    /// """
    /// order by stddev() on columns of table "links"
    /// """
    /// input links_stddev_order_by {
    /// from_id: order_by
    ///     id: order_by
    ///     to_id: order_by
    ///     type_id: order_by
    /// }
    ///
    /// """
    /// order by stddev_pop() on columns of table "links"
    /// """
    /// input links_stddev_pop_order_by {
    /// from_id: order_by
    ///     id: order_by
    ///     to_id: order_by
    ///     type_id: order_by
    /// }
    ///
    /// """
    /// order by stddev_samp() on columns of table "links"
    /// """
    /// input links_stddev_samp_order_by {
    /// from_id: order_by
    ///     id: order_by
    ///     to_id: order_by
    ///     type_id: order_by
    /// }
    ///
    /// """
    /// order by var_pop() on columns of table "links"
    /// """
    /// input links_var_pop_order_by {
    /// from_id: order_by
    ///     id: order_by
    ///     to_id: order_by
    ///     type_id: order_by
    /// }
    ///
    /// """
    /// order by var_samp() on columns of table "links"
    /// """
    /// input links_var_samp_order_by {
    /// from_id: order_by
    ///     id: order_by
    ///     to_id: order_by
    ///     type_id: order_by
    /// }
    /// </remarks>
    internal class LinksFieldsOrderByInputType : InputObjectGraphType<LinksFieldsOrderBy>
    {
        public LinksFieldsOrderByInputType()
        {
            Field(x => x.id, nullable: true, type: typeof(OrderByEnumType));
            Field(x => x.from_id, nullable: true, type: typeof(OrderByEnumType));
            Field(x => x.to_id, nullable: true, type: typeof(OrderByEnumType));
            Field(x => x.type_id, nullable: true, type: typeof(OrderByEnumType));
        }
    }
}
