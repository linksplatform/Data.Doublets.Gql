using Gql.Samples.Schemas.Link;
using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema
{

    internal class LinksSelectColumnEnumType : EnumerationGraphType<LinksSelectColumn>
    {
        public LinksSelectColumnEnumType() => Name = "LinksSelectColumnEnum";
    }
}
