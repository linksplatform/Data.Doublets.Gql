using System;
using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema
{
    public class LinkSchema : GraphQL.Types.Schema
    {
        public LinkSchema(ILinks Link, IServiceProvider provider) : base(provider)
        {
            Query = new LinkQuery(Link);
            Mutation = new LinkMutation(Link);
        }
    }
}
