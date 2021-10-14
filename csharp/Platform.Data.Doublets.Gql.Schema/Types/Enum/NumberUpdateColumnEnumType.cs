using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Enum
{
    public class NumberUpdateColumnEnumType : EnumerationGraphType<NumberUpdateColumn>
    {
        protected override string ChangeEnumCase(string value)
        {
            return value.ToSnakeCase();
        }

        public NumberUpdateColumnEnumType(){}
        public NumberUpdateColumnEnumType(string name) => Name = name;
    }
}
