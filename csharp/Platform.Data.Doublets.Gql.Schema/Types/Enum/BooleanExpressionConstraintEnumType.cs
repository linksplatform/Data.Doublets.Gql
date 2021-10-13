using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Enum
{
    public class BooleanExpressionConstraintEnumType : EnumerationGraphType<BooleanExpressionConstraint>
    {
        protected override string ChangeEnumCase(string value)
        {
            return value.ToSnakeCase();
        }

        public BooleanExpressionConstraintEnumType(){}
        public BooleanExpressionConstraintEnumType(string name) => Name = name;
    }
}
