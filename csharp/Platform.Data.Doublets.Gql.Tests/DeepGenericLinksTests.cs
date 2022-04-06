using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Platform.Data.Doublets.Decorators;
using Platform.Data.Doublets.Gql.Client;
using Platform.Data.Doublets.Tests;
using Platform.IO;
using Platform.Memory;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using Xunit;
using TLinkAddress = System.UInt64;

namespace Platform.Data.Doublets.Gql.Tests;

public class DeepGenericLinksTests
{
    public readonly string TempFilePath;
    public readonly Uri EndPoint = new Uri("");

    public DeepGenericLinksTests()
    {
        TempFilePath = new TemporaryFile();
    }

    [Fact (Skip = "")]
    public void CRUDTest()
    {
        Using(links =>
        {
            var allLinks = links.All();
            foreach (var linkToDelete in allLinks)
            {
                var id = linkToDelete![0];
                if (links.Exists(id))
                {
                    links.Delete(id);
                }
            }
            links.TestCRUDOperations();
        });
    }

    [Fact (Skip = "")]
    public void RawNumbersCRUDTest()
    {
        Using(links =>
        {
            var allLinks = links.All();
            foreach (var linkToDelete in allLinks)
            {
                var id = linkToDelete![0];
                if (links.Exists(id))
                {
                    links.Delete(id);
                }
            }
            links.TestRawNumbersCRUDOperations();
        });
    }

    [Fact (Skip = "")]
    public void MultipleRandomCreationsAndDeletionsTest()
    {
        Using(links =>
        {
            var allLinks = links.All();
            foreach (var linkToDelete in allLinks)
            {
                var id = linkToDelete![0];
                if (links.Exists(id))
                {
                    links.Delete(id);
                }
            }
            links.TestMultipleRandomCreationsAndDeletions(10);
        });
    }

    [Fact (Skip = "")]
    public void MultipleRandomCreationsAndDeletionsWithDecoratorsTest()
    {
        Using(links =>
        {
            var allLinks = links.All();
            foreach (var linkToDelete in allLinks)
            {
                var id = linkToDelete![0];
                if (links.Exists(id))
                {
                    links.Delete(id);
                }
            }
            links.DecorateWithAutomaticUniquenessAndUsagesResolution().TestMultipleRandomCreationsAndDeletions(10);
        });
    }

    private void Using(Action<ILinks<TLinkAddress>> action)
    {
        var graphqlClient = new GraphQLHttpClient(EndPoint, new NewtonsoftJsonSerializer());
        var linksConstants = new LinksConstants<TLinkAddress>(true);
        var token = "";
        var linksGqlStorage = new DeepGqlAdapter(graphqlClient, linksConstants, token);
        using var logFile = File.Open("linksLogger.txt", FileMode.Create, FileAccess.Write);
        LoggingDecorator<TLinkAddress> decoratedLinksStorage = new(linksGqlStorage, logFile);
        action(decoratedLinksStorage);
    }
}
