using GraphQL;
using GraphQL.SystemTextJson;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Platform.Data.Doublets.Gql.Client;
using Platform.Data.Doublets.Gql.Schema;
using Platform.Data.Doublets.Memory;
using Platform.Data.Doublets.Memory.United.Generic;
using Platform.IO;
using Platform.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using TLinkAddress = System.UInt64;

namespace Platform.Data.Doublets.Gql.Tests
{
    public class MutationTests
    {
        public static EqualityComparer<TLinkAddress> EqualityComparer = EqualityComparer<TLinkAddress>.Default;
        public static ILinks<ulong> CreateLinks() => CreateLinks<ulong>(new TemporaryFile());

        public static ILinks<TLinkAddress> CreateLinks<TLinkAddress>(string dataDBFilename)
        {
            var linksConstants = new LinksConstants<TLinkAddress>(true);
            return new UnitedMemoryLinks<TLinkAddress>(new FileMappedResizableDirectMemory(dataDBFilename), UnitedMemoryLinks<TLinkAddress>.DefaultLinksSizeStep, linksConstants, IndexTreeType.Default);
        }

        [Fact]
        public void InsertLinksOne()
        {
            var links = CreateLinks();
            LinksSchema linksSchema = new(links, new DefaultServiceProvider());
            var jsonTask = linksSchema.ExecuteAsync(_ => { _.Query = @"
        mutation {
          insert_links_one(object: {from_id: 1, to_id: 1}) {
            id
    	    from_id
    	    to_id
          }
        }
        "; });
            dynamic result = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(jsonTask.Result);
            if (result.ContainsKey("errors"))
            {
                throw new Exception(result.errors.ToString());
            }
        }

        [Fact]
        public void InsertLinks()
        {
            var links = CreateLinks();
            LinksSchema linksSchema = new(links, new DefaultServiceProvider());
            var jsonTask = linksSchema.ExecuteAsync(_ => { _.Query = @"
        mutation {
          insert_links(objects: [{ from_id: 1, to_id: 1 }, { from_id: 2, to_id: 2 }]) {
            returning {
              id
              from_id
              to_id
            }
          }
        }
        "; });
            dynamic result = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(jsonTask.Result);
            if (result.ContainsKey("errors"))
            {
                throw new Exception(result.errors.ToString());
            }
        }

        [Fact]
        public void UpdateLinks()
        {
            var links = CreateLinks();
            LinksSchema linksSchema = new(links, new DefaultServiceProvider());
            var jsonTask = linksSchema.ExecuteAsync(_ => { _.Query = @"
        mutation {
          update_links(_set: { from_id: 1, to_id: 2 }, where: { from_id: { _eq: 2 }, to_id: { _eq: 2 } }) {
            returning {
              id
              from_id
              to_idв
            }
          }
        }
        "; });
            dynamic result = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(jsonTask.Result);
            if (result.ContainsKey("errors"))
            {
                throw new Exception(result.errors.ToString());
            }
        }

        [Fact]
        public void DeleteLinks()
        {

            var links = CreateLinks();
            LinksSchema linksSchema = new(links, new DefaultServiceProvider());
            var jsonTask = linksSchema.ExecuteAsync(_ => { _.Query = @"
        mutation {
          delete_links(where: { from_id: { _eq: 1 }, to_id: { _eq: 1 } }) {
            returning {
              id
              from_id
              to_id
            }
          }
        }
        "; });
            dynamic result = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(jsonTask.Result);
            if (result.ContainsKey("errors"))
            {
                throw new Exception(result.errors.ToString());
            }
        }

        [Fact]
        public void CreateZeroZeroAndUpdateToOneOneById()
        {
            var links = CreateLinks();
            LinksSchema linksSchema = new(links, new DefaultServiceProvider());
            var jsonTask = linksSchema.ExecuteAsync(_ => { _.Query = @"
            mutation {
              insert_links_one(object: {from_id: 0, to_id: 0}) {
                id
                from_id
                to_id
              }
            }
            "; });
            var jsonSerializer = new JsonSerializer();
            var jsonResponse = jsonTask.Result;
            Assert.False(JObject.Parse(jsonResponse).ContainsKey("errors"));
            jsonTask = linksSchema.ExecuteAsync(_ => { _.Query = @"
            mutation {
              update_links(_set: { from_id: 1, to_id: 1 }, where: { id: {_eq: 1} }) {
                returning {
                  id
                  from_id
                  to_id
                }
              }
            }
            "; });
            dynamic result = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(jsonTask.Result);
            if (result.ContainsKey("errors"))
            {
                throw new Exception(result.errors.ToString());
            }
            Assert.True(1 == Convert.ToInt32(result.data.update_links.returning[0].id));
        }
    }
}
