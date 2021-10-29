using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    using MappedType = Links;
    /// <remarks>
    /// """
    /// columns and relationships of "links"
    /// """
    /// type links {
    ///   """An array relationship"""
    ///   _by_group(
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
    ///   _by_group_aggregate(
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
    ///   _by_item(
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
    ///   _by_item_aggregate(
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
    ///   _by_path_item(
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
    ///   _by_path_item_aggregate(
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
    ///   _by_root(
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
    ///   _by_root_aggregate(
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
    ///   """An object relationship"""
    ///   bool_exp: bool_exp
    ///
    ///   """An object relationship"""
    ///   from: links
    ///   from_id: bigint
    ///   id: bigint!
    ///
    ///   """An array relationship"""
    ///   in(
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
    ///   in_aggregate(
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
    ///   """An object relationship"""
    ///   number: number
    ///
    ///   """An array relationship"""
    ///   out(
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
    ///   out_aggregate(
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
    ///   """An object relationship"""
    ///   string: string
    ///
    ///   """An object relationship"""
    ///   to: links
    ///   to_id: bigint
    ///
    ///   """An object relationship"""
    ///   type: links
    ///   type_id: bigint!
    /// }
    /// </remarks>
    public class LinksInputType : InputObjectGraphType<MappedType>
    {
        public LinksInputType()
        {
            Field<LongGraphType>(nameof(MappedType.id));
            Field<LongGraphType>(nameof(MappedType.from_id));
            Field<LongGraphType>(nameof(MappedType.to_id));
            Field<LongGraphType>(nameof(MappedType.type_id));
        }
    }
}
