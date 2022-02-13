using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Platform.Data.Doublets.Gql.Client;
using Platform.Data.Doublets.Gql.Server;
using Platform.Data.Doublets.Memory;
using Platform.Data.Doublets.Memory.United.Generic;
using Platform.Memory;
using Serilog;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Xunit;
using TLinkAddress = System.UInt64;

namespace Platform.Data.Doublets.Gql.Tests;

public class ClientTests
{
    private readonly ulong _any;
    private readonly LinksGqlAdapter<ulong> _linksGqlAdapter;


    public ClientTests()
    {
        var linksConstants = new LinksConstants<TLinkAddress>(enableExternalReferencesSupport: true);
        _linksGqlAdapter = new LinksGqlAdapter<TLinkAddress>(new GraphQLHttpClient("http://localhost:60341/v1/graphql", new NewtonsoftJsonSerializer()), linksConstants);
        var processInfo = new ProcessStartInfo();
        processInfo.FileName = "dotnet";
        processInfo.Arguments = $"run db.links";
        processInfo.WorkingDirectory = Path.GetDirectoryName(Assembly.GetAssembly(typeof(Server.Program)).Location);
        processInfo.UseShellExecute = false;
        var a = Process.Start(processInfo);
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
            return _linksGqlAdapter.Constants.Break;
        }, new Link<ulong>(_any, _any, _any));
        Assert.Equal(count, eachIterations);
    }
}
