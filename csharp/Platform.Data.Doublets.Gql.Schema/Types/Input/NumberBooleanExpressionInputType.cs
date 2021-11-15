using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    public class NumberBooleanExpressionInputType : InputObjectGraphType<NumberBooleanExpression>
    {
        public NumberBooleanExpressionInputType()
        {
            Name = "number_bool_exp";
            Field<ListGraphType<NumberBooleanExpressionInputType>>(nameof(MappedType._and));
            Field<NumberBooleanExpressionInputType>(nameof(MappedType._not));
            Field<ListGraphType<NumberBooleanExpressionInputType>>(nameof(MappedType._or));
            Field<LongComparisonExpressionInputType>(nameof(MappedType.id));
            Field<LinksBooleanExpressionInputType>(nameof(MappedType.link));
            Field<LongComparisonExpressionInputType>(nameof(MappedType.link_id));
            Field<DoubleComparisonExpressionInputType>(nameof(MappedType.value));
        }
    }
}
