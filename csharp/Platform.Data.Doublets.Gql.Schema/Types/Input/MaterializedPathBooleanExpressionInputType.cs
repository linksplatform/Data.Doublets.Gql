using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    using MappedType = MaterializedPathBooleanExpression;
    public class MaterializedPathBooleanExpressionInputType : InputObjectGraphType<MappedType>
    {
        public MaterializedPathBooleanExpressionInputType()
        {
            Name = "mp_bool_exp";
            Field<ListGraphType<MaterializedPathBooleanExpressionInputType>>(nameof(MappedType._and));
            Field<MaterializedPathBooleanExpressionInputType>(nameof(MappedType._not));
            Field<ListGraphType<MaterializedPathBooleanExpressionInputType>>(nameof(MappedType._or));
            Field<LinksBooleanExpressionInputType>(nameof(MappedType.by_group));
            Field<MaterializedPathBooleanExpressionInputType>(nameof(MappedType.by_item));
            Field<MaterializedPathBooleanExpressionInputType>(nameof(MappedType.by_path_item));
            Field<MaterializedPathBooleanExpressionInputType>(nameof(MappedType.by_position));
            Field<MaterializedPathBooleanExpressionInputType>(nameof(MappedType.by_root));
            Field<LongComparisonExpressionInputType>(nameof(MappedType.group_id));
            Field<LongComparisonExpressionInputType>(nameof(MappedType.id));
            Field<StringComparisonExpressionInputType>(nameof(MappedType.insert_category));
            Field<LinksBooleanExpressionInputType>(nameof(MappedType.item));
            Field<LongComparisonExpressionInputType>(nameof(MappedType.item_id));
            Field<LinksBooleanExpressionInputType>(nameof(MappedType.path_item));
            Field<LongComparisonExpressionInputType>(nameof(MappedType.path_item_depth));
            Field<LongComparisonExpressionInputType>(nameof(MappedType.path_item_id));
            Field<StringComparisonExpressionInputType>(nameof(MappedType.position_id));
            Field<LinksBooleanExpressionInputType>(nameof(MappedType.root));
            Field<LongComparisonExpressionInputType>(nameof(MappedType.root_id));
        }
    }
}
