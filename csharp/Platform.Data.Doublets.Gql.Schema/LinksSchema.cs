using System;

namespace Platform.Data.Doublets.Gql.Schema
{
    public class LinksSchema : GraphQL.Types.Schema
    {
        public LinksSchema(ILinks<ulong> links, IServiceProvider provider) : base(provider)
        {
            Query = new LinksQuery(links);
            Mutation = new LinksMutation(links);
            Subscription = new LinksSubscription(links);
        }
    }
}