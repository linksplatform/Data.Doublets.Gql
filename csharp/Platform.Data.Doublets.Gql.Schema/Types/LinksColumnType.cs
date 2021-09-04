using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types
{

    internal class LinksColumnType : EnumerationGraphType<LinksColumn>
    {
        public LinksColumnType() => Name = "LinksSelectColumnEnum";
    }
}
