using GraphQL;
using GraphQL.SystemTextJson;
using Newtonsoft.Json.Linq;
using Platform.Data.Doublets.Gql.Schema;
using Platform.Data.Doublets.Memory;
using Platform.Data.Doublets.Memory.United.Generic;
using Platform.IO;
using Platform.Memory;
using Xunit;
using TLinkAddress = System.UInt64;

namespace Platform.Data.Doublets.Gql.Tests
{
    public class DeepQueryTests
    {
        public static ILinks<ulong> CreateLinks() => CreateLinks<ulong>(new TemporaryFile());

        public static ILinks<TLinkAddress> CreateLinks<TLinkAddress>(string dataDbFilename)
        {
            var linksConstants = new LinksConstants<TLinkAddress>(true);
            return new UnitedMemoryLinks<TLinkAddress>(new FileMappedResizableDirectMemory(dataDbFilename), UnitedMemoryLinks<TLinkAddress>.DefaultLinksSizeStep, linksConstants, IndexTreeType.Default);
        }

        [Fact]
        public void TestDeepNestedWhereQuery()
        {
            // Test query with nested from/to conditions
            var query = @"
            {
              links(
                where: {
                  from: { id: { _eq: 1 } }
                  to: { id: { _gt: 5 } }
                }
              ) {
                id
                from_id
                to_id
                from { id }
                to { id }
              }
            }
            ";
            
            var links = CreateLinks();
            LinksSchema linksSchema = new(links, new DefaultServiceProvider());
            var jsonTask = linksSchema.ExecuteAsync(_ => { _.Query = query; });
            var response = JObject.Parse(jsonTask.Result);
            var error = response.ContainsKey("errors");
            Assert.False(error, $"Query failed with errors: {jsonTask.Result}");
        }

        [Fact]
        public void TestLogicalOperatorsQuery()
        {
            // Test query with _and and _or operators
            var query = @"
            {
              links(
                where: {
                  _and: [
                    { id: { _gt: 0 } }
                    { _or: [
                      { from_id: { _eq: 1 } }
                      { to_id: { _eq: 2 } }
                    ] }
                  ]
                }
              ) {
                id
                from_id
                to_id
              }
            }
            ";
            
            var links = CreateLinks();
            LinksSchema linksSchema = new(links, new DefaultServiceProvider());
            var jsonTask = linksSchema.ExecuteAsync(_ => { _.Query = query; });
            var response = JObject.Parse(jsonTask.Result);
            var error = response.ContainsKey("errors");
            Assert.False(error, $"Query failed with errors: {jsonTask.Result}");
        }

        [Fact]
        public void TestOutInRelationshipQuery()
        {
            // Test query with @out and @in relationship filters
            var query = @"
            {
              links(
                where: {
                  out: { to_id: { _eq: 3 } }
                  in: { from_id: { _eq: 2 } }
                }
              ) {
                id
                from_id
                to_id
                out { id, to_id }
                in { id, from_id }
              }
            }
            ";
            
            var links = CreateLinks();
            LinksSchema linksSchema = new(links, new DefaultServiceProvider());
            var jsonTask = linksSchema.ExecuteAsync(_ => { _.Query = query; });
            var response = JObject.Parse(jsonTask.Result);
            var error = response.ContainsKey("errors");
            Assert.False(error, $"Query failed with errors: {jsonTask.Result}");
        }
    }
}