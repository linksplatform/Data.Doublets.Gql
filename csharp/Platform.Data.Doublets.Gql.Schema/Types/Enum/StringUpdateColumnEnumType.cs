using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Enum
{
    public class StringUpdateColumnEnumType : EnumerationGraphType<StringUpdateColumn>
    {
        protected override string ChangeEnumCase(string value)
        {
            return value.ToSnakeCase();
        }

        public StringUpdateColumnEnumType(){}
        public StringUpdateColumnEnumType(string name) => Name = name;
    }
}
