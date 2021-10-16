using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Enum
{
    public class BaseEnumType<T> : EnumerationGraphType<T> where T : System.Enum
    {
        protected override string ChangeEnumCase(string value)
        {
            return value.ToSnakeCase();
        }

        public BaseEnumType(string name) => Name = name;

        public BaseEnumType(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
