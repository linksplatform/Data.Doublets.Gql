using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Enum
{
    public class LinksColumnEnumType : EnumerationGraphType<LinksColumn>
    {
        public LinksColumnEnumType()
        {
        }

        public LinksColumnEnumType(string name)
        {
            Name = name;
        }
    }
}