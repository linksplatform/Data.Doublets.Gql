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
using System.Threading;
using Xunit;
using TLinkAddress = System.UInt64;

namespace Platform.Data.Doublets.Gql.Tests;

public class ClientTests : IDisposable
{
    private readonly ulong _any;
    private readonly LinksGqlAdapter<ulong> _linksGqlAdapter;
    private Process? _serverProcess;
    private const string EndPoint = "http://localhost:60341/v1/graphql";

    public void Dispose()
    {
        _serverProcess?.CloseMainWindow();
        _serverProcess?.Dispose();
        // _serverProcess?.Kill();
    }

    private Process RunServer()
    {
        var currentAssemblyDirectory = Directory.GetCurrentDirectory();
        var currentProjectDirectory = Path.GetFullPath(Path.Combine(currentAssemblyDirectory, "..", "..", ".."));
        var serverProjectDirectory = Path.GetFullPath(Path.Combine(currentProjectDirectory, "..", "Platform.Data.Doublets.Gql.Server"));
        var tempFilePath = IO.TemporaryFiles.UseNew();
        var processStartInfo = new ProcessStartInfo { WorkingDirectory = serverProjectDirectory, FileName = "dotnet", Arguments = $"run -f net5 {tempFilePath}", UseShellExecute = true};
        var process = Process.Start(processStartInfo);
        return process;
    }

    private Uri GetEndPoint(Process process)
    {
        Uri endPoint;
        while (true)
        {
            var standartOutput = process?.StandardOutput;
            if(standartOutput == null)
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));
                continue;
            }
            var processOutputLine = standartOutput.ReadLine();
            if (string.IsNullOrEmpty(processOutputLine))
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));
                continue;
            }
            if (processOutputLine.Contains("Unable to start"))
            {
                throw new Exception("Unable to start.");
            }
            if (processOutputLine.Contains("Now listening on: "))
            {
                var index = processOutputLine.IndexOf("Now listening on: ") + "Now listening on: ".Length;
                var uriString = processOutputLine.Substring(index);
                endPoint = new Uri(uriString);
                return endPoint;
            }
        }
    }

    private void TestCud()
    {
        // _serverProcess = RunServer();
        var graphQlClient = new GraphQLHttpClient(EndPoint, new NewtonsoftJsonSerializer());
        var linksGqlAdapter = new LinksGqlAdapter<TLinkAddress>(graphQlClient, new LinksConstants<TLinkAddress>(true));

        ulong linksAmount = 5;
        // Create
        for (ulong i = 1; i <= linksAmount; i++)
        {
            ulong one = 1;
            // Create
            var createdLink = linksGqlAdapter.CreateAndUpdate(one, i);
            // Count
            Assert.Equal(i, createdLink);
            Assert.Equal(i, linksGqlAdapter.Count());
            Assert.Equal(i, linksGqlAdapter.Count(one, _any));
        }
    }

    [Fact]
    public void CudTest()
    {

        TestCud();
    }

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
