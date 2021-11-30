using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    using MappedType = BooleanExpressionBooleanExpression;

    public class BooleanExpressionBooleanExpressionInputType : InputObjectGraphType<MappedType>
    {
        public BooleanExpressionBooleanExpressionInputType()
        {
            Name = "bool_exp_bool_exp";
            Field<ListGraphType<BooleanExpressionBooleanExpressionInputType>>(nameof(MappedType._and));
            Field<BooleanExpressionBooleanExpressionInputType>(nameof(MappedType._not));
            Field<ListGraphType<BooleanExpressionBooleanExpressionInputType>>(nameof(MappedType._or));
            Field<StringComparisonExpressionInputType>(nameof(MappedType.gql));
            Field<LongComparisonExpressionInputType>(nameof(MappedType.id));
            Field<LinksBooleanExpressionInputType>(nameof(MappedType.link));
            Field<LongComparisonExpressionInputType>(nameof(MappedType.link_id));
            Field<StringComparisonExpressionInputType>(nameof(MappedType.sql));
        }
    }
}
