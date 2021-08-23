using GraphQL.Types;
using Platform.Data.Doublets.Gql.Schema.Types;

namespace Platform.Data.Doublets.Gql.Schema
{
    internal class LinksSubscription : ObjectGraphType
    {
        public LinksSubscription(ILinks<ulong> links) => Field<ListGraphType<LinkType>>("links",
        arguments: LinkQuery.Arguments,
        resolve: context =>
        {
            return LinkQuery.GetLinks(context, links);
        });
    }
}
