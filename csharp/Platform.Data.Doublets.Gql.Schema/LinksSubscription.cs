using GraphQL.Types;
using Platform.Data.Doublets.Gql.Schema.Types;

namespace Platform.Data.Doublets.Gql.Schema
{
    /// <remarks>
    /// """subscription root"""
    /// type subscription_root {
    ///   """
    ///   fetch data from the table: "links"
    ///   """
    ///   links(
    ///     """distinct select on columns"""
    ///     distinct_on: [links_select_column!]
    ///
    ///     """limit the number of rows returned"""
    ///     limit: Int
    ///
    ///     """skip the first n rows. Use only with order_by"""
    ///     offset: Int
    ///
    ///     """sort the rows by one or more columns"""
    ///     order_by: [links_order_by!]
    ///
    ///     """filter the rows returned"""
    ///     where: links_bool_exp
    ///   ): [links!]!
    ///
    ///   """
    ///   fetch aggregated fields from the table: "links"
    ///   """
    ///   links_aggregate(
    ///     """distinct select on columns"""
    ///     distinct_on: [links_select_column!]
    ///
    ///     """limit the number of rows returned"""
    ///     limit: Int
    ///
    ///     """skip the first n rows. Use only with order_by"""
    ///     offset: Int
    ///
    ///     """sort the rows by one or more columns"""
    ///     order_by: [links_order_by!]
    ///
    ///     """filter the rows returned"""
    ///     where: links_bool_exp
    ///   ): links_aggregate!
    ///
    ///   """fetch data from the table: "links" using primary key columns"""
    ///   links_by_pk(id: bigint!): links
    /// }
    /// </remarks>
    public class LinksSubscription : ObjectGraphType
    {
        public LinksSubscription(ILinks<ulong> links)
        {
            Name = "subscription_root";
            Field<ListGraphType<LinksType>>("links",
                arguments: LinksQuery.Arguments,
                resolve: context => { return LinksQuery.GetLinks(context, links); });
        }
    }
}
