using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Enum
{
    public class NumberConstraintEnumType : EnumerationGraphType<NumberConstraint>
    {
        protected override string ChangeEnumCase(string value)
        {
            return value.ToSnakeCase();
        }
        public NumberConstraintEnumType(){}
        public NumberConstraintEnumType(string name) => Name = name;
    }
}
