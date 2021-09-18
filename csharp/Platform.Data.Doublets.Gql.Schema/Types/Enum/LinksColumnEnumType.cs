using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema
{
    public class LinksColumnEnumType : EnumerationGraphType<LinksColumn>
    {
        public LinksColumnEnumType(string name) => Name = name;
    }
}
