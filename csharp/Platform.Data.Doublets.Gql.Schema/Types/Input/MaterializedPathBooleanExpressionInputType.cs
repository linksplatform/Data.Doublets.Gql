using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    /// <remarks>
    ///     """
    ///     Boolean expression to filter rows from the table "mp". All fields are combined with a logical 'AND'.
    ///     """
    ///     input mp_bool_exp {
    ///     _and: [mp_bool_exp]
    ///     _not: mp_bool_exp
    ///     _or: [mp_bool_exp]
    ///     by_group: links_bool_exp
    ///     by_item: mp_bool_exp
    ///     by_path_item: mp_bool_exp
    ///     by_position: mp_bool_exp
    ///     by_root: mp_bool_exp
    ///     group_id: bigint_comparison_exp
    ///     id: bigint_comparison_exp
    ///     insert_category: String_comparison_exp
    ///     item: links_bool_exp
    ///     item_id: bigint_comparison_exp
    ///     path_item: links_bool_exp
    ///     path_item_depth: bigint_comparison_exp
    ///     path_item_id: bigint_comparison_exp
    ///     position_id: String_comparison_exp
    ///     root: links_bool_exp
    ///     root_id: bigint_comparison_exp
    ///     }
    /// </remarks>
    public class MaterializedPathBooleanExpressionInputType : InputObjectGraphType<MaterializedPathBooleanExpression>
    {
        public MaterializedPathBooleanExpressionInputType()
        {
            Name = "mp_bool_exp";
            Field(x => x._and, true, typeof(ListGraphType<MaterializedPathBooleanExpressionInputType>));
            Field(x => x._not, true, typeof(MaterializedPathBooleanExpressionInputType));
            Field(x => x._or, true, typeof(ListGraphType<MaterializedPathBooleanExpressionInputType>));
            Field(x => x.by_group, true, typeof(LinksBooleanExpressionInputType));
            Field(x => x.by_item, true, typeof(MaterializedPathBooleanExpressionInputType));
            Field(x => x.by_path_item, true, typeof(MaterializedPathBooleanExpressionInputType));
            Field(x => x.by_position, true, typeof(MaterializedPathBooleanExpressionInputType));
            Field(x => x.by_root, true, typeof(MaterializedPathBooleanExpressionInputType));
            Field(x => x.group_id, true, typeof(LongComparisonExpressionInputType));
            Field(x => x.id, true, typeof(LongComparisonExpressionInputType));
            Field(x => x.insert_category, true, typeof(StringComparisonExpressionInputType));
            Field(x => x.item, true, typeof(LinksBooleanExpressionInputType));
            Field(x => x.item_id, true, typeof(LongComparisonExpressionInputType));
            Field(x => x.path_item, true, typeof(LinksBooleanExpressionInputType));
            Field(x => x.path_item_depth, true, typeof(LongComparisonExpressionInputType));
            Field(x => x.path_item_id, true, typeof(LongComparisonExpressionInputType));
            Field(x => x.position_id, true, typeof(StringComparisonExpressionInputType));
            Field(x => x.root, true, typeof(LinksBooleanExpressionInputType));
            Field(x => x.root_id, true, typeof(LongComparisonExpressionInputType));
        }
    }
}
