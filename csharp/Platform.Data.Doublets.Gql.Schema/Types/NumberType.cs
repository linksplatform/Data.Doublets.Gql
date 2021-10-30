/* using GraphQL.Types;
using Platform.Data.Doublets.Gql.Schema.Types.Enum;
using Platform.Data.Doublets.Gql.Schema.Types.Input;

namespace Platform.Data.Doublets.Gql.Schema.Types
{
    using MappedType = Number;

    /// <remarks>
    /// """
    /// columns and relationships of "number"
    /// """
    /// type number {
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
    ///   value: float8
    /// }
    /// </remarks>
    public class NumberType : ObjectGraphType<MappedType>
    {
        public NumberType()
        {
            Name = "number";
            Description = "columns and relationships of \"number\"";
            Field<NonNullGraphType<LongGraphType>>(nameof(MappedType.id));
            Field<ListGraphType<LinksType>>(nameof(MappedType.link), arguments: new QueryArguments
            {
                new QueryArgument<NonNullGraphType<ListGraphType<NonNullGraphType<LinksType>>>>
                    { Name = "distinct_on" },
                new QueryArgument<IntGraphType> { Name = "limit" },
                new QueryArgument<IntGraphType> { Name = "offset" },
                new QueryArgument<ListGraphType<NonNullGraphType<LinksOrderByInputType>>> { Name = "order_by" },
                new QueryArgument<LinksBooleanExpressionInputType> { Name = "where" }
            });
            Field<LinksAggregateType>(nameof(MappedType.link_aggregate), arguments: new QueryArguments
            {
                new QueryArgument<ListGraphType<NonNullGraphType<LinksSelectColumnEnumBaseType>>> { Name = "distinct_on" },
                new QueryArgument<IntGraphType> { Name = "limit" },
                new QueryArgument<IntGraphType> { Name = "offset" },
                new QueryArgument<ListGraphType<NonNullGraphType<LinksOrderByInputType>>> { Name = "order_by" },
                new QueryArgument<LinksBooleanExpressionInputType> { Name = "where" }
            });
            Field(x => x.link_id, nullable: true, type: typeof(LongGraphType));
            Field(x => x.value, nullable: true, type: typeof());
        }
    }
}
*/
