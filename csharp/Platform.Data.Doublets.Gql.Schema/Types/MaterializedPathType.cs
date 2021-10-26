/* using GraphQL.Types;
using Platform.Data.Doublets.Gql.Schema.Types.Enum;

namespace Platform.Data.Doublets.Gql.Schema.Types
{
    /// <remarks>
    /// """
    /// columns and relationships of "mp"
    /// """
    /// type mp {
    ///   """An object relationship"""
    ///   by_group: links
    ///
    ///   """An array relationship"""
    ///   by_item(
    ///     """distinct select on columns"""
    ///     distinct_on: [mp_select_column!]
    ///
    ///     """limit the number of rows returned"""
    ///     limit: Int
    ///
    ///     """skip the first n rows. Use only with order_by"""
    ///     offset: Int
    ///
    ///     """sort the rows by one or more columns"""
    ///     order_by: [mp_order_by!]
    ///
    ///     """filter the rows returned"""
    ///     where: mp_bool_exp
    ///   ): [mp!]!
    ///
    ///   """An aggregated array relationship"""
    ///   by_item_aggregate(
    ///     """distinct select on columns"""
    ///     distinct_on: [mp_select_column!]
    ///
    ///     """limit the number of rows returned"""
    ///     limit: Int
    ///
    ///     """skip the first n rows. Use only with order_by"""
    ///     offset: Int
    ///
    ///     """sort the rows by one or more columns"""
    ///     order_by: [mp_order_by!]
    ///
    ///     """filter the rows returned"""
    ///     where: mp_bool_exp
    ///   ): mp_aggregate!
    ///
    ///   """An array relationship"""
    ///   by_path_item(
    ///     """distinct select on columns"""
    ///     distinct_on: [mp_select_column!]
    ///
    ///     """limit the number of rows returned"""
    ///     limit: Int
    ///
    ///     """skip the first n rows. Use only with order_by"""
    ///     offset: Int
    ///
    ///     """sort the rows by one or more columns"""
    ///     order_by: [mp_order_by!]
    ///
    ///     """filter the rows returned"""
    ///     where: mp_bool_exp
    ///   ): [mp!]!
    ///
    ///   """An aggregated array relationship"""
    ///   by_path_item_aggregate(
    ///     """distinct select on columns"""
    ///     distinct_on: [mp_select_column!]
    ///
    ///     """limit the number of rows returned"""
    ///     limit: Int
    ///
    ///     """skip the first n rows. Use only with order_by"""
    ///     offset: Int
    ///
    ///     """sort the rows by one or more columns"""
    ///     order_by: [mp_order_by!]
    ///
    ///     """filter the rows returned"""
    ///     where: mp_bool_exp
    ///   ): mp_aggregate!
    ///
    ///   """An array relationship"""
    ///   by_position(
    ///     """distinct select on columns"""
    ///     distinct_on: [mp_select_column!]
    ///
    ///     """limit the number of rows returned"""
    ///     limit: Int
    ///
    ///     """skip the first n rows. Use only with order_by"""
    ///     offset: Int
    ///
    ///     """sort the rows by one or more columns"""
    ///     order_by: [mp_order_by!]
    ///
    ///     """filter the rows returned"""
    ///     where: mp_bool_exp
    ///   ): [mp!]!
    ///
    ///   """An aggregated array relationship"""
    ///   by_position_aggregate(
    ///     """distinct select on columns"""
    ///     distinct_on: [mp_select_column!]
    ///
    ///     """limit the number of rows returned"""
    ///     limit: Int
    ///
    ///     """skip the first n rows. Use only with order_by"""
    ///     offset: Int
    ///
    ///     """sort the rows by one or more columns"""
    ///     order_by: [mp_order_by!]
    ///
    ///     """filter the rows returned"""
    ///     where: mp_bool_exp
    ///   ): mp_aggregate!
    ///
    ///   """An array relationship"""
    ///   by_root(
    ///     """distinct select on columns"""
    ///     distinct_on: [mp_select_column!]
    ///
    ///     """limit the number of rows returned"""
    ///     limit: Int
    ///
    ///     """skip the first n rows. Use only with order_by"""
    ///     offset: Int
    ///
    ///     """sort the rows by one or more columns"""
    ///     order_by: [mp_order_by!]
    ///
    ///     """filter the rows returned"""
    ///     where: mp_bool_exp
    ///   ): [mp!]!
    ///
    ///   """An aggregated array relationship"""
    ///   by_root_aggregate(
    ///     """distinct select on columns"""
    ///     distinct_on: [mp_select_column!]
    ///
    ///     """limit the number of rows returned"""
    ///     limit: Int
    ///
    ///     """skip the first n rows. Use only with order_by"""
    ///     offset: Int
    ///
    ///     """sort the rows by one or more columns"""
    ///     order_by: [mp_order_by!]
    ///
    ///     """filter the rows returned"""
    ///     where: mp_bool_exp
    ///   ): mp_aggregate!
    ///   group_id: bigint
    ///   id: bigint!
    ///   insert_category: String
    ///
    ///   """An object relationship"""
    ///   item: links
    ///   item_id: bigint
    ///
    ///   """An object relationship"""
    ///   path_item: links
    ///   path_item_depth: bigint
    ///   path_item_id: bigint
    ///   position_id: String
    ///
    ///   """An object relationship"""
    ///   root: links
    ///   root_id: bigint
    /// }
    /// </remarks>
    public class MaterializedPathType : ObjectGraphType<MaterializedPath>
    {
        public MaterializedPathType()
        {
            Name = "mp";
            Description = "columns and relationships of \"mp\"";
            Field(x => x.by_group, nullable: true, type: typeof(LinksType));
            Field("by_item", arguments: new QueryArguments
            {
                new QueryArgument<MaterializedPathSelectColumnEnumType>{Name = "distinct_on", Description = "distinct select on columns"},
                new QueryArgument<IntGraphType>{Name = "limit", Description = "limit the number of rows returned"},
                new QueryArgument<IntGraphType>{Name = "offset", Description = "skip the first n rows. Use only with order_by"},
                new QueryArgument<ListGraphType<NonNullGraphType<MPOB>>>{Name = "order_by", Description = "sort the rows by one or more columns"},
                new QueryArgument<LinksBooleanExpressionInputType>{Name = "where", Description = "filter the rows returned"}
            })
        }
    }
}
*/
