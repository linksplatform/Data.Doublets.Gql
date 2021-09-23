using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema
{
    /// <remarks>
    ///     """column ordering options"""
    ///     enum order_by {
    ///     """in the ascending order, nulls last"""
    ///     asc
    ///     """in the ascending order, nulls first"""
    ///     asc_nulls_first
    ///     """in the ascending order, nulls last"""
    ///     asc_nulls_last
    ///     """in the descending order, nulls first"""
    ///     desc
    ///     """in the descending order, nulls first"""
    ///     desc_nulls_first
    ///     """in the descending order, nulls last"""
    ///     desc_nulls_last
    ///     }
    /// </remarks>
    public class OrderByEnumType : EnumerationGraphType<order_by>
    {
        public OrderByEnumType()
        {
            Name = "order_by";
            Description = "column ordering options";
            AddValue(nameof(order_by.asc).ToSnakeCase(), "in the ascending order, nulls last", order_by.asc);
            AddValue(nameof(order_by.asc_nulls_first).ToSnakeCase(), "in the ascending order, nulls first", order_by.asc_nulls_first);
            AddValue(nameof(order_by.asc_nulls_last).ToSnakeCase(), "in the ascending order, nulls last", order_by.asc_nulls_last);
            AddValue(nameof(order_by.desc).ToSnakeCase(), "in the descending order, nulls first", order_by.desc);
            AddValue(nameof(order_by.desc_nulls_first).ToSnakeCase(), "in the descending order, nulls first", order_by.desc_nulls_first);
            AddValue(nameof(order_by.desc_nulls_last).ToSnakeCase(), "in the descending order, nulls last", order_by.desc_nulls_last);
        }
    }
}
