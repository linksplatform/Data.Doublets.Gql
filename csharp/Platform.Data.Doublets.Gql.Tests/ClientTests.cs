using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Platform.Data.Doublets.Gql.Client;
using Platform.Data.Doublets.Gql.Server;
using Xunit;
using TLinkAddress = System.UInt64;

namespace Platform.Data.Doublets.Gql.Tests;

public class ClientTests
{
    private readonly ulong _any;
    private readonly LinksConstants<ulong> _linksConstants;
    private readonly LinksGqlAdapter<ulong> _linksGqlAdapter;

    public ClientTests()
    {
        Program.Main(new[] { "db.links" });
        GraphQLHttpClient graphQlHttpClient = new("http://localhost:60341/v1/graphql", new NewtonsoftJsonSerializer());
        _linksConstants = new LinksConstants<ulong>(true);
        _linksGqlAdapter = new LinksGqlAdapter<ulong>(graphQlHttpClient, _linksConstants);
        _any = _linksConstants.Any;
    }

    private void TestCud()
    {
        ulong linksAmount = 5;
        // Create
        for (ulong i = 1; i <= linksAmount; i++)
        {
            ulong one = 1;
            // Create
            var createdLink = _linksGqlAdapter.CreateAndUpdate(one, i);
            // Count
            Assert.Equal(i, createdLink);
            Assert.Equal(i, _linksGqlAdapter.Count());
            Assert.Equal(i, _linksGqlAdapter.Count(one, _any));
        }
    }

    [Fact]
    public void CudTest() => TestCud();

    [Fact]
    public void EachTest()
    {
        TestCud();
        var count = _linksGqlAdapter.Count();
        ulong eachIterations = 0;
        _linksGqlAdapter.Each(link =>
        {
            Assert.Equal(++eachIterations, _linksGqlAdapter.GetTarget(link));
            return _linksConstants.Break;
        }, new Link<ulong>(_any, _any, _any));
        Assert.Equal(count, eachIterations);
    }
}
