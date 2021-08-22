using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    internal class LinksMutationResponseType : ObjectGraphType
    {
        public LinksMutationResponseType()
        {
            Field<IntGraphType>("affected_rows");
            Field<ListGraphType<LinkType>>("returning");
        }
    }
}
