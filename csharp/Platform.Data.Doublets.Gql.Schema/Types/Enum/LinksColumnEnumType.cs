using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Enum
{
    public class LinksColumnEnumType : EnumerationGraphType<LinksColumn>
    {
        protected override string ChangeEnumCase(string value)
        {
            return value.ToSnakeCase();
        }
        public LinksColumnEnumType()
        {
        }

        public LinksColumnEnumType(string name)
        {
            Name = name;
        }
    }
}
