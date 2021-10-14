using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Enum
{
    public class StringConstraintEnumType : EnumerationGraphType<StringConstraint>
    {
        protected override string ChangeEnumCase(string value)
        {
            return value.ToSnakeCase();
        }
        public StringConstraintEnumType(){}
        public StringConstraintEnumType(string name) => Name = name;
    }
}
