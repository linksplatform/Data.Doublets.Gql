using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{

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
