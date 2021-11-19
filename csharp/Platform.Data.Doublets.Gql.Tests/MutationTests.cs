using GraphQL;
using GraphQL.SystemTextJson;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;
using Platform.Data.Doublets.Gql.Schema;
using Platform.Data.Doublets.Gql.Server;
using Platform.Data.Doublets.Memory;
using Platform.Data.Doublets.Memory.United.Generic;
using Platform.Memory;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using TLink = System.UInt64;

namespace Platform.Data.Doublets.Gql.Tests
{
    public class MutationTests
    {
        public static ILinks<TLink> CreateLinks() => CreateLinks<TLink>(new Platform.IO.TemporaryFile());

        public static ILinks<TLink> CreateLinks<TLink>(string dataDBFilename)
        {
            var linksConstants = new LinksConstants<TLink>(enableExternalReferencesSupport: true);
            return new UnitedMemoryLinks<TLink>(new FileMappedResizableDirectMemory(dataDBFilename), UnitedMemoryLinks<TLink>.DefaultLinksSizeStep, linksConstants, IndexTreeType.Default);
        }

        [InlineData(@"
        mutation {
          insert_links_one(object: {from_id: 1, to_id: 1}) {
            returning{
              id
    	        from_id
    	        to_id
            }
          }
        }
        ")]
        [InlineData(@"
        mutation {
          insert_links(objects: [{ from_id: 1, to_id: 1 }, { from_id: 2, to_id: 2 }]) {
            returning {
              id
              from_id
              to_id
            }
          }
        }
        ")]
        [InlineData(@"
        mutation {
          update_links(_set: { from_id: 1, to_id: 2 }, where: { from_id: { _eq: 2 }, to_id: { _eq: 2 } }) {
            returning {
              id
              from_id
              to_id
            }
          }
        }
        ")]
        [InlineData(@"
        mutation {
          delete_links(where: { from_id: { _eq: 1 }, to_id: { _eq: 1 } }) {
            returning {
              id
              from_id
              to_id
            }
          }
        }
        ")]
        [Theory]
        public void MutateData(string query)
        {
            var links = CreateLinks();
            LinksSchema linksSchema = new (links, new DefaultServiceProvider());
            var jsonTask = linksSchema.ExecuteAsync(_ =>
                {
                    _.Query = query;
                });
            var response = JObject.Parse(jsonTask.Result);
            var error = response.ContainsKey("errors");
            Assert.False(error);
        }
    }
}
