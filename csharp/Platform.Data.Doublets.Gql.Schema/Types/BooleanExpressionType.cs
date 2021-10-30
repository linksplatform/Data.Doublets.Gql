/* using GraphQL.Types;
using Platform.Data.Doublets.Gql.Schema.Types.Enum;
using Platform.Data.Doublets.Gql.Schema.Types.Input;

namespace Platform.Data.Doublets.Gql.Schema.Types
{
    /// <remarks>
    /// """
    /// columns and relationships of "bool_exp"
    /// """
    /// type bool_exp {
    ///   gql: String
    ///   id: bigint!
    ///
    ///   """An array relationship"""
    ///   link(
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
    ///   """An aggregated array relationship"""
    ///   link_aggregate(
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
    ///   link_id: bigint
    ///   sql: String
    /// }
    /// </remarks>
    public class BooleanExpressionType : ObjectGraphType<BooleanExpression>
    {
        public BooleanExpressionType()
        {
            Name = "bool_exp";
            Description = "columns and relationships of \"bool_exp\"";
            Field(x => x.gql, nullable: true, type: typeof(StringGraphType));
            Field<NonNullGraphType<LongGraphType>>("id");
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<LinksType>>>>("link", arguments: new QueryArguments
            {
                new QueryArgument<ListGraphType<NonNullGraphType<LinksSelectColumnEnumBaseType>>> { Name = "distinct_on", Description = "distinct select on columns"},
                new QueryArgument<IntGraphType>{Name = "limit", Description = "limit the number of rows returned"},
                new QueryArgument<IntGraphType>{Name = "offset", Description = "skip the first n rows. Use only with order_by"},
                new QueryArgument<ListGraphType<NonNullGraphType<LinksOrderByInputType>>>{Name = "order_by", Description = "sort the rows by one or more columns"},
                new QueryArgument<LinksBooleanExpressionInputType>{Name = "where", Description = "filter the rows returned"}
            });
        }
    }
}
*/
