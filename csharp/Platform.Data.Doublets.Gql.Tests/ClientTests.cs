using GraphQL.Client.Http;
using Platform.Data.Doublets.Gql.Client;
using GraphQL.Client.Serializer.Newtonsoft;
using System.Collections.Generic;
using Xunit;
using TLink = System.UInt64;

namespace Platform.Data.Doublets.Gql.Tests;

public class ClientTests
{
    [Fact]
    public void CreateTest()
    {
        GraphQLHttpClient graphQlHttpClient = new("http://localhost:60341/v1/graphql", new NewtonsoftJsonSerializer());
        LinksConstants<TLink> linksConstants = new (true);
        LinksGqlAdapter<TLink> linksGqlAdapter = new LinksGqlAdapter<ulong>(graphQlHttpClient, linksConstants);
        var createdLink = linksGqlAdapter.Create(new List<ulong> { 1, 1 });
        Assert.Equal("(1: 1 1)", createdLink.ToString());
    }
}
