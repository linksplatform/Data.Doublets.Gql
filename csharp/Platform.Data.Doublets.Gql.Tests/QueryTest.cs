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
    public class QueryTests
    {
        public static ILinks<ulong> CreateLinks() => CreateLinks<ulong>(new TemporaryFile());

        public static ILinks<TLinkAddress> CreateLinks<TLinkAddress>(string dataDbFilename)
        {
            var linksConstants = new LinksConstants<TLinkAddress>(true);
            return new UnitedMemoryLinks<TLinkAddress>(new FileMappedResizableDirectMemory(dataDbFilename), UnitedMemoryLinks<TLinkAddress>.DefaultLinksSizeStep, linksConstants, IndexTreeType.Default);
        }

        [InlineData(@"
        {
          links {
            id
          }
        }
        ")]
        [InlineData(@"
        {
          links(
            where: { id: { _eq: 1 }, from_id: { _eq: 1 }, to_id: { _eq: 1 } }
            distinct_on: [from_id]
            order_by: { id: asc }
            offset: 0
            limit: 1
          ) {
            id
            from_id
            from {
              id
              from_id
              to_id
            }
            out {
              id
              from_id
              to_id
            }
            to_id
            to {
              id
              from_id
              to_id
            }
            in {
              id
              from_id
              to_id
            }
          }
        }
        ")]
        [InlineData(@"
        {
          links(
            where: { id: { _eq: 1 }, from_id: { _eq: 1 }, to_id: { _eq: 1 } }
            distinct_on: [from_id]
            order_by: { id: asc }
            offset: 0
            limit: 1
          ) {
            id
            from_id
            from {
              id
              from_id
              to_id
            }
            out(
              where: { from_id: { _eq: 1 }, to_id: { _eq: 1 } }
              distinct_on: [from_id]
              order_by: { id: asc }
              offset: 0
              limit: 1
            ) {
              id
              from_id
              to_id
            }
            to_id
            to {
              id
              from_id
              to_id
            }
            in(
              where: { from_id: { _eq: 1 }, to_id: { _eq: 1 } }
              distinct_on: [from_id]
              order_by: { id: asc }
              offset: 0
              limit: 1
            ) {
              id
              from_id
              to_id
            }
          }
        }
        ")]
        [InlineData(@"
        {
          links(
            where: { 
              _and: [
                { id: { _gt: 0 } }
                { from_id: { _eq: 1 } }
              ]
            }
          ) {
            id
            from_id
            to_id
          }
        }
        ")]
        [InlineData(@"
        {
          links(
            where: { 
              _or: [
                { from_id: { _eq: 1 } }
                { to_id: { _eq: 2 } }
              ]
            }
          ) {
            id
            from_id
            to_id
          }
        }
        ")]
        [InlineData(@"
        {
          links(
            where: { 
              _not: { id: { _eq: 0 } }
            }
          ) {
            id
            from_id
            to_id
          }
        }
        ")]
        [InlineData(@"
        {
          links(
            where: { 
              from: { id: { _gt: 0 } }
            }
          ) {
            id
            from_id
            to_id
            from {
              id
              from_id
              to_id
            }
          }
        }
        ")]
        [InlineData(@"
        {
          links(
            where: { 
              to: { id: { _lte: 10 } }
            }
          ) {
            id
            from_id
            to_id
            to {
              id
              from_id
              to_id
            }
          }
        }
        ")]
        [InlineData(@"
        {
          links(
            where: { 
              out: { to_id: { _gt: 0 } }
            }
          ) {
            id
            from_id
            to_id
            out {
              id
              from_id
              to_id
            }
          }
        }
        ")]
        [InlineData(@"
        {
          links(
            where: { 
              in: { from_id: { _gt: 0 } }
            }
          ) {
            id
            from_id
            to_id
            in {
              id
              from_id
              to_id
            }
          }
        }
        ")]
        [InlineData(@"
        {
          links(
            where: { 
              _and: [
                { from: { id: { _gt: 0 } } }
                { to: { id: { _lt: 100 } } }
              ]
            }
          ) {
            id
            from_id
            to_id
            from { id }
            to { id }
          }
        }
        ")]
        [Theory]
        public void QueryData(string query)
        {
            var links = CreateLinks();
            LinksSchema linksSchema = new(links, new DefaultServiceProvider());
            var jsonTask = linksSchema.ExecuteAsync(_ => { _.Query = query; });
            var response = JObject.Parse(jsonTask.Result);
            var error = response.ContainsKey("errors");
            Assert.False(error);
        }
    }
}
