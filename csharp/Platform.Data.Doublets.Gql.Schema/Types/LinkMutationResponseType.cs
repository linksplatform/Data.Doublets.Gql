using GraphQL.Types;

namespace Input
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
