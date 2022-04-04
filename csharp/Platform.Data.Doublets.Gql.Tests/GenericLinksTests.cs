using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Platform.Data.Doublets.Decorators;
using Platform.Data.Doublets.Gql.Client;
using Platform.Data.Doublets.Tests;
using Platform.IO;
using Platform.Memory;
using System;
using System.Diagnostics;
using System.IO;
using Xunit;

namespace Platform.Data.Doublets.Gql.Tests;

public class GenericLinksTests : IDisposable
{
    public readonly string TempFilePath;
    public readonly string TempFilePath1;
    public readonly Uri EndPoint;
    public readonly Process ServerProcess;

    public GenericLinksTests()
    {
        TempFilePath = new TemporaryFile();
        ServerProcess = TestExtensions.RunServer(TempFilePath);
        EndPoint = TestExtensions.GetEndPointFromServerProcess(ServerProcess);
    }

    public void Dispose()
    {
        ServerProcess.Kill(true);
    }

    [Fact]
    public void CRUDTest()
    {
        Using(links => links.TestCRUDOperations());
    }

    [Fact]
    public void RawNumbersCRUDTest()
    {
        Using(links => links.TestRawNumbersCRUDOperations());
    }

    [Fact]
    public void MultipleRandomCreationsAndDeletionsTest()
    {
        Using(links => links.DecorateWithAutomaticUniquenessAndUsagesResolution().TestMultipleRandomCreationsAndDeletions(7));
    }
    private void Using(Action<ILinks<ulong>> action)
    {
        var graphqlClient = new GraphQLHttpClient(EndPoint, new NewtonsoftJsonSerializer());
        var linksConstants = new LinksConstants<ulong>(true);
        var linksGqlStorage = new LinksGqlAdapter(graphqlClient, linksConstants);
        using var logFile = File.Open("linksLogger.txt", FileMode.Create, FileAccess.Write);
        LoggingDecorator<ulong> decoratedLinksStorage = new(linksGqlStorage, logFile);
        action(decoratedLinksStorage);
    }
}
