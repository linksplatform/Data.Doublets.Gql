using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    using MappedType = StringBooleanExpression;
    public class StringBooleanExpressionInputType : InputObjectGraphType<MappedType>
    {
        public StringBooleanExpressionInputType()
        {
            Name = "string_bool_exp";
            Field<ListGraphType<StringBooleanExpressionInputType>>(nameof(MappedType._and));
            Field<StringBooleanExpressionInputType>(nameof(MappedType._not));
            Field<ListGraphType<StringBooleanExpressionInputType>>(nameof(MappedType._or));
            Field<LongComparisonExpressionInputType>(nameof(MappedType.id));
            Field<LinksBooleanExpressionInputType>(nameof(MappedType.link));
            Field<LongComparisonExpressionInputType>(nameof(MappedType.link_id));
            Field<StringComparisonExpressionInputType>(nameof(MappedType.value));
        }
    }
}
