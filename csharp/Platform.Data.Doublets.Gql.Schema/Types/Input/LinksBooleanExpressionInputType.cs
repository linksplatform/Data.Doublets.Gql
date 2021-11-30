using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    using MappedType = LinksBooleanExpression;

    public class LinksBooleanExpressionInputType : InputObjectGraphType<MappedType>
    {
        public LinksBooleanExpressionInputType()
        {
            Name = "links_bool_exp";
            Field<ListGraphType<LinksBooleanExpressionInputType>>(nameof(MappedType._and));
            Field<MaterializedPathBooleanExpressionInputType>(nameof(MappedType._by_group));
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
