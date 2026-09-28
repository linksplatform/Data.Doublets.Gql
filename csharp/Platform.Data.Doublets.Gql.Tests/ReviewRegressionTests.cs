using GraphQL;
using GraphQL.SystemTextJson;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using Platform.Data.Doublets.Gql.Schema;
using Platform.Data.Doublets.Memory;
using Platform.Data.Doublets.Memory.United.Generic;
using Platform.Memory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Platform.Data.Doublets.Gql.Tests
{
    public sealed class ReviewRegressionTests
    {
        [Fact]
        public async Task VariablesPreserveNestedLogicalAndComparisonSiblings()
        {
            using var graph = new ReviewGraph();
            var filter = new Dictionary<string, object>
            {
                ["_or"] = new[] { new Dictionary<string, object>() },
                ["_not"] = new Dictionary<string, object> { ["id"] = new Dictionary<string, object> { ["_eq"] = 2L } },
                ["from"] = new Dictionary<string, object> { ["to_id"] = new Dictionary<string, object> { ["_eq"] = 2L } },
                ["id"] = new Dictionary<string, object> { ["_gte"] = 3L, ["_lte"] = 5L }
            };
            var result = await graph.Execute("query($filter:links_bool_exp!){links(where:$filter,order_by:{id:desc}){id}}",
                new Inputs(new Dictionary<string, object> { ["filter"] = filter }));
            AssertNoErrors(result);
            Assert.Equal(new long[] { 5, 4 }, Ids(result["data"]!["links"]!));
        }

        [Fact]
        public async Task DeMorganEquivalentPredicatesReturnTheSameSeededLinks()
        {
            using var graph = new ReviewGraph();
            var result = await graph.Execute(@"{
                negated:links(where:{_not:{_or:[{from_id:{_eq:1}},{to_id:{_eq:1}}]}},order_by:{id:asc}){id}
                conjunction:links(where:{_and:[{_not:{from_id:{_eq:1}}},{_not:{to_id:{_eq:1}}}]},order_by:{id:asc}){id}
            }");
            AssertNoErrors(result);
            Assert.Equal(new long[] { 2, 5, 6, 7 }, Ids(result["data"]!["negated"]!));
            Assert.Equal(Ids(result["data"]!["negated"]!), Ids(result["data"]!["conjunction"]!));
        }

        [Fact]
        public async Task DeepLogicalMutationChangesOnlyTheSingleSelectedRow()
        {
            using var graph = new ReviewGraph();
            var before = await graph.Execute("{links(order_by:{id:asc}){id from_id to_id}}");
            AssertNoErrors(before);
            var changed = await graph.Execute(@"mutation {
                update_links(where:{_and:[],_or:[{from:{to_id:{_eq:2}}}],id:{_gt:4,_lt:6}},_set:{from_id:4,to_id:3}) {
                    affected_rows returning{id from_id to_id}
                }
            }");
            AssertNoErrors(changed);
            Assert.Equal(1, (int)changed["data"]!["update_links"]!["affected_rows"]!);
            Assert.Equal(new long[] { 5 }, Ids(changed["data"]!["update_links"]!["returning"]!));
            var after = await graph.Execute("{links(order_by:{id:asc}){id from_id to_id}}");
            AssertNoErrors(after);
            var original = (JArray)before["data"]!["links"]!;
            original[4]!["from_id"] = 4;
            original[4]!["to_id"] = 3;
            Assert.True(JToken.DeepEquals(original, after["data"]!["links"]!), after.ToString());
        }

        [Fact]
        public async Task InvalidPredicatesAreValidatedEvenOnEmptyGraphOrFalseBranch()
        {
            using var graph = new ReviewGraph(seed: false);
            foreach (var where in new[] { "{_or:[],type:{}}", "{_and:[{_or:[null]}]}", "{_not:{type_id:{_eq:0}}}" })
            {
                var result = await graph.Execute("mutation{delete_links(where:" + where + "){affected_rows}}");
                Assert.NotEmpty(result["errors"]!);
            }
            var remaining = await graph.Execute("{links{id}}");
            AssertNoErrors(remaining);
            Assert.Empty(remaining["data"]!["links"]!);
        }

        private static void AssertNoErrors(JObject result) => Assert.False(result.ContainsKey("errors"), result.ToString());
        private static long[] Ids(JToken rows) => rows.Select(row => (long)row["id"]!).ToArray();

        private sealed class ReviewGraph : IDisposable
        {
            private readonly string _path = Path.Combine(Path.GetTempPath(), "doublets-review-" + Guid.NewGuid() + ".links");
            private readonly ILinks<ulong> _links;
            private readonly LinksSchema _schema;
            private readonly ServiceProvider _services;
            public ReviewGraph(bool seed = true)
            {
                _links = new UnitedMemoryLinks<ulong>(new FileMappedResizableDirectMemory(_path), UnitedMemoryLinks<ulong>.DefaultLinksSizeStep,
                    new LinksConstants<ulong>(true), IndexTreeType.Default);
                _services = new ServiceCollection().AddSingleton(_links).BuildServiceProvider();
                _schema = new LinksSchema(_links, new DefaultServiceProvider());
                if (!seed) return;
                var pairs = new (ulong From, ulong To)[] { (1, 1), (2, 2), (1, 2), (2, 1), (3, 4), (0, 0), (99, 99) };
                for (var i = 0; i < pairs.Length; i++) Assert.Equal((ulong)i + 1, _links.GetOrCreate(pairs[i].From, pairs[i].To));
            }
            public async Task<JObject> Execute(string query, Inputs? variables = null) => JObject.Parse(await _schema.ExecuteAsync(options =>
            {
                options.Query = query;
                options.Inputs = variables;
                options.RequestServices = _services;
            }));
            public void Dispose()
            {
                _schema.Dispose();
                _services.Dispose();
                (_links as IDisposable)?.Dispose();
                File.Delete(_path);
            }
        }
    }
}
