using GraphQL;
using GraphQL.Client.Abstractions;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace Platform.Data.Doublets.Gql.Client
{
    public class LinksGqlAdapter<TLink> : ILinks<TLink>
    {
        private IGraphQLClient _graphQlClient;

        public LinksGqlAdapter(IGraphQLClient graphQlClient, LinksConstants<TLink> linksConstants)
        {
            _graphQlClient = graphQlClient;
            Constants = linksConstants;
        }

        public TLink Count(IList<TLink> restriction)
        {
            throw new NotImplementedException();
        }

        public TLink Each(Func<IList<TLink>, TLink> handler, IList<TLink> restrictions) => throw new NotImplementedException();

        public TLink Create(IList<TLink> restrictions) => throw new NotImplementedException();

        public TLink Update(IList<TLink> restrictions, IList<TLink> substitution) => throw new NotImplementedException();

        public void Delete(IList<TLink> restrictions) => throw new NotImplementedException();

        public LinksConstants<TLink> Constants { get; }
    }

    public struct CountResponseType
    {
        public BigInteger id;
    }
}
