using GraphQL.Types;
using System;

namespace Platform.Data.Doublets.Gql.Schema.Types.Enums
{
    public class BaseEnumType<T> : EnumerationGraphType<T> where T : Enum
    {
        public BaseEnumType(string name) => Name = name;

        public BaseEnumType(string name, string description)
        {
            Name = name;
            Description = description;
        }

        protected override string ChangeEnumCase(string value) => value.ToSnakeCase();
    }
}
