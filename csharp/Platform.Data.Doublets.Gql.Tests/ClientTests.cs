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
    private readonly Uri _endPoint;

    public ClientTests()
    {
        _constants = new LinksConstants<TLinkAddress>(true);
        _serverProcess = RunServer();
        _endPoint = GetEndPoint(_serverProcess);
        var graphQlClient = new GraphQLHttpClient(_endPoint, new NewtonsoftJsonSerializer());
        _linksGqlAdapter = new LinksGqlAdapter<TLinkAddress>(graphQlClient, _constants);
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
        var processStartInfo = new ProcessStartInfo { WorkingDirectory = serverProjectDirectory, FileName = "dotnet", Arguments = $"run -f net5 {tempFilePath}", RedirectStandardOutput = true};
        var process = Process.Start(processStartInfo);
        if (null == process)
        {
            throw new Exception("Fail to start server process");
        }
        return process;
    }

    private Uri GetEndPoint(Process process)
    {
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
                return new Uri($"{uriString}/v1/graphql");
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
            var allLinks = new Link<Link<TLinkAddress>>();
            _linksGqlAdapter.Each(link =>
            {
                allLinks.Add(new Link<TLinkAddress>(link));
                return _constants.Continue;
            });
            Assert.Equal(i, _linksGqlAdapter.Count(i, _constants.Any));
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
