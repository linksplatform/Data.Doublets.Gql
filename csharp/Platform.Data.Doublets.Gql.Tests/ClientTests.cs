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
    private readonly LinksConstants<TLinkAddress> _constants;
    private readonly LinksGqlAdapter<TLinkAddress> _linksGqlAdapter;
    private readonly Process _serverProcess;
    private const string EndPoint = "http://localhost:60341/v1/graphql";

    public ClientTests()
    {
        _constants = new LinksConstants<TLinkAddress>(true);
        var graphQlClient = new GraphQLHttpClient(EndPoint, new NewtonsoftJsonSerializer());
        _linksGqlAdapter = new LinksGqlAdapter<TLinkAddress>(graphQlClient, _constants);
        _serverProcess = RunServer();
    }

    public void Dispose()
    {
        _serverProcess.CloseMainWindow();
        _serverProcess.Dispose();
    }

    private Process RunServer()
    {
        var currentAssemblyDirectory = Directory.GetCurrentDirectory();
        var currentProjectDirectory = Path.GetFullPath(Path.Combine(currentAssemblyDirectory, "..", "..", ".."));
        var serverProjectDirectory = Path.GetFullPath(Path.Combine(currentProjectDirectory, "..", "Platform.Data.Doublets.Gql.Server"));
        var tempFilePath = IO.TemporaryFiles.UseNew();
        var processStartInfo = new ProcessStartInfo { WorkingDirectory = serverProjectDirectory, FileName = "dotnet", Arguments = $"run -f net5 {tempFilePath}", UseShellExecute = true};
        var process = Process.Start(processStartInfo);
        if (null == process)
        {
            throw new Exception("Fail to start server process");
        }
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
        TLinkAddress linksAmount = 5;
        // Create
        for (TLinkAddress i = 1; i <= linksAmount; i++)
        {
            TLinkAddress one = 1;
            // Create
            var createdLink = _linksGqlAdapter.CreateAndUpdate(one, i);
            // Count
            Assert.Equal(i, createdLink);
            Assert.Equal(i, _linksGqlAdapter.Count());
            Assert.Equal(i, _linksGqlAdapter.Count(one, _constants.Any));
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
        TLinkAddress eachIterations = 0;
        _linksGqlAdapter.Each(link =>
        {
            Assert.Equal(++eachIterations, _linksGqlAdapter.GetTarget(link));
            return _linksGqlAdapter.Constants.Break;
        }, new Link<TLinkAddress>(_constants.Any, _constants.Any, _constants.Any));
        Assert.Equal(count, eachIterations);
    }
}
