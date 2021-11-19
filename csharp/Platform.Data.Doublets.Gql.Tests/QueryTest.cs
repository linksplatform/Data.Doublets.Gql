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
    public class QueryTests
    {
        /// <summary>
        /// <para>
        /// Creates the links.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <returns>
        /// <para>A links of t link</para>
        /// <para></para>
        /// </returns>
        public static ILinks<TLink> CreateLinks() => CreateLinks<TLink>(new Platform.IO.TemporaryFile());

        /// <summary>
        /// <para>
        /// Creates the links using the specified data db filename.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <typeparam name="TLink">
        /// <para>The link.</para>
        /// <para></para>
        /// </typeparam>
        /// <param name="dataDBFilename">
        /// <para>The data db filename.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>A links of t link</para>
        /// <para></para>
        /// </returns>
        public static ILinks<TLink> CreateLinks<TLink>(string dataDBFilename)
        {
            var linksConstants = new LinksConstants<TLink>(enableExternalReferencesSupport: true);
            return new UnitedMemoryLinks<TLink>(new FileMappedResizableDirectMemory(dataDBFilename), UnitedMemoryLinks<TLink>.DefaultLinksSizeStep, linksConstants, IndexTreeType.Default);
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
        [Theory]
        public void QueryData(string query)
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
