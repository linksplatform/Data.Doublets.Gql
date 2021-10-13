using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Enum
{
    public class BooleanExpressionUpdateColumnEnumType : EnumerationGraphType<BooleanExpressionUpdateColumn>
    {
        protected override string ChangeEnumCase(string value)
        {
            return value.ToSnakeCase();
        }

        public BooleanExpressionUpdateColumnEnumType(){}
        public BooleanExpressionUpdateColumnEnumType(string name) => Name = name;
    }
}
