using System;

namespace Platform.Data.Doublets.Gql.Schema
{
    public class LinksSchema : GraphQL.Types.Schema
    {
        public LinksSchema(ILinks<ulong> links, IServiceProvider provider) : base(provider)
        {
            var subscription = new LinksSubscription(links);
            Query = new LinksQuery(links);
            Mutation = new LinksMutation(links, subscription);
            Subscription = subscription;
        }
    }
}
