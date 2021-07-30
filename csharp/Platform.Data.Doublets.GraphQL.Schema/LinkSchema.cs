using System;
using GraphQL.Types;

namespace GraphQL.Samples.Schemas.Link
{
    public class LinkSchema : Schema
    {
        public LinkSchema(ILinks Link, IServiceProvider provider) : base(provider)
        {
            Query = new LinkQuery(Link);
            //Mutation = new LinkMutation(Link);
            //Subscription = new LinkSubscriptions();
        }
    }
}
