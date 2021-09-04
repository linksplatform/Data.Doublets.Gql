using System;

namespace Platform.Data.Doublets.Gql.Schema
{
    public class LinkSchema : GraphQL.Types.Schema
    {
        public LinkSchema(ILinks<ulong> links, IServiceProvider provider) : base(provider)
        {
            Query = new LinkQuery(links);
            Mutation = new LinkMutation(links);
            //Subscription = new LinksSubscription(links);
        }
    }
}
