using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema
{

    internal class LinksColumnType : EnumerationGraphType<LinksColumn>
    {
        public LinksColumnType() => Name = "LinksSelectColumnEnum";
    }
}
