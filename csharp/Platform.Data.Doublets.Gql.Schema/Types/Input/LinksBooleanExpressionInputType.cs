using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    using MappedType = LinksBooleanExpression;

    /// <remarks>
    ///     """
    ///     Boolean expression to filter rows from the table "links". All fields are combined with a logical 'AND'.
    ///     """
    ///     input links_bool_exp {
    ///     _and: [links_bool_exp]
    ///     _not: links_bool_exp
    ///     _or: [links_bool_exp]
    ///     from: links_bool_exp
    ///     from_id: bigint_comparison_exp
    ///     id: bigint_comparison_exp
    ///     in: links_bool_exp
    ///     out: links_bool_exp
    ///     to: links_bool_exp
    ///     to_id: bigint_comparison_exp
    ///     type: links_bool_exp
    ///     type_id: bigint_comparison_exp
    ///     }
    /// </remarks>
    public class LinksBooleanExpressionInputType : InputObjectGraphType<MappedType>
    {
        public LinksBooleanExpressionInputType()
        {
            Name = "links_bool_exp";
            Field<ListGraphType<LinksBooleanExpressionInputType>>(nameof(MappedType._and));
            Field(x => x._by_group, true, typeof(MaterializedPathBooleanExpressionInputType));
            Field<LinksBooleanExpressionInputType>(nameof(MappedType._not));
            Field<ListGraphType<LinksBooleanExpressionInputType>>(nameof(MappedType._or));
            Field<LinksBooleanExpressionInputType>(nameof(MappedType.from));
            Field<LongComparisonExpressionInputType>(nameof(MappedType.from_id));
            Field<LongComparisonExpressionInputType>(nameof(MappedType.id));
            Field<LinksBooleanExpressionInputType>(nameof(MappedType.@in));
            Field<LinksBooleanExpressionInputType>(nameof(MappedType.@out));
            Field<LinksBooleanExpressionInputType>(nameof(MappedType.to));
            Field<LongComparisonExpressionInputType>(nameof(MappedType.to_id));
            Field<LinksBooleanExpressionInputType>(nameof(MappedType.type));
            Field<LongComparisonExpressionInputType>(nameof(MappedType.type_id));
        }
    }
}
