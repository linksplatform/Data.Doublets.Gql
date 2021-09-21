using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema
{
    /// <remarks>
    ///     """column ordering options"""
    ///     enum OrderBy {
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
    public class OrderByEnumType : EnumerationGraphType<OrderBy>
    {
        public OrderByEnumType()
        {
            Name = "OrderBy";
        }
    }
}
