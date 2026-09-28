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
    public sealed class DeepWhereTests
    {
        public static IEnumerable<object[]> PredicateCases()
        {
            yield return Case("{_and:[],id:{_gt:3}}", 4, 5, 6, 7);
            yield return Case("{_or:[]}");
            yield return Case("{_or:[{id:{_eq:1}}],id:{_gt:2}}");
            yield return Case("{_not:{id:{_eq:2}},_or:[{id:{_eq:2}}]}");
            yield return Case("{_and:[{id:{_gt:1}}],_or:[{id:{_eq:1}},{id:{_eq:3}}],to_id:{_eq:2}}", 3);
            yield return Case("{_not:{id:{_eq:2}},to_id:{_eq:2}}", 3, 4);
            yield return Case("{from:{from:{from:{id:{_eq:1}}}}}", 1, 3, 4, 5);
            yield return Case("{to:{from:{id:{_eq:1}}}}", 1, 5, 6);
            yield return Case("{out:{to_id:{_eq:2}}}", 1, 2, 3);
            yield return Case("{in:{from:{id:{_eq:1}}}}", 1, 2);
            yield return Case("{out:{_and:[],to_id:{_gt:999}}}");
            yield return Case("{from:{id:{_eq:999}}}");
            yield return Case("{from:{}}", 1, 2, 3, 4, 5, 6);
            yield return Case("{_not:{from:{}}}", 7);
            yield return Case("{id:{_eq:3,_gt:3}}");
            yield return Case("{id:{_neq:3,_gte:2,_lte:4}}", 2, 4);
            yield return Case("{id:{_in:[1,3,5],_nin:[3]}}", 1, 5);
            yield return Case("{id:{_in:[]}}");
            yield return Case("{id:{_nin:[]}}", 1, 2, 3, 4, 5, 6, 7);
            yield return Case("{id:{_lt:0}}");
            yield return Case("{id:{_eq:-1}}");
            yield return Case("{from_id:{_eq:-1}}");
            yield return Case("{id:{_gt:-1}}", 1, 2, 3, 4, 5, 6, 7);
            yield return Case("{from_id:{_is_null:true}}", 7);
            yield return Case("{from_id:{_is_null:false,_lte:1}}", 1, 3);
        }

        private static object[] Case(string where, params long[] expected) => new object[] { where, expected };

        [Theory]
        [MemberData(nameof(PredicateCases))]
        public async Task DeepPredicatesReturnExactSeededIds(string where, long[] expected)
        {
            using var fixture = new GraphFixture();
            var result = await fixture.Query("{links(where:" + where + ",order_by:{id:asc}){id}}");
            Assert.Equal(expected, Ids(result["links"]!));
        }

        [Fact]
        public async Task ParentRelationshipScopeAppliesWithoutWhereAndCannotBeEscapedByOr()
        {
            using var fixture = new GraphFixture();
            var result = await fixture.Query(@"{links(where:{id:{_eq:1}}){
                out{id} in{id}
                filtered:out(where:{_or:[{id:{_eq:2}},{id:{_eq:3}}]}){id}
                impossible:out(where:{from_id:{_eq:2}}){id}
            }}");
            var row = result["links"]![0]!;
            Assert.Equal(new long[] { 1, 3 }, Ids(row["out"]!));
            Assert.Equal(new long[] { 1, 6 }, Ids(row["in"]!));
            Assert.Equal(new long[] { 3 }, Ids(row["filtered"]!));
            Assert.Empty(Ids(row["impossible"]!));
        }

        [Fact]
        public async Task DeepFilteringPrecedesOrderingAndPagination()
        {
            using var fixture = new GraphFixture();
            var result = await fixture.Query("{links(where:{from:{from:{id:{_eq:1}}}},order_by:{id:desc},offset:1,limit:1){id}}");
            Assert.Equal(new long[] { 3 }, Ids(result["links"]!));
        }

        [Fact]
        public async Task MissingReferencesAreNotRelationshipsAndNotCanMatchThem()
        {
            using var fixture = new GraphFixture();
            var missing = fixture.Links.GetOrCreate(99UL, 99UL);
            var result = await fixture.Query("{links(where:{id:{_eq:" + missing + "},from:{}}){id}}");
            Assert.Empty(Ids(result["links"]!));
            result = await fixture.Query("{links(where:{id:{_eq:" + missing + "},_not:{from:{}}}){id}}");
            Assert.Equal(new long[] { (long)missing }, Ids(result["links"]!));
        }

        [Fact]
        public async Task DeepUpdateSelectsAllIdsBeforeChangingReferencedRows()
        {
            using var fixture = new GraphFixture(seed: false);
            fixture.Links.GetOrCreate(1UL, 1UL);
            fixture.Links.GetOrCreate(2UL, 2UL);
            fixture.Links.GetOrCreate(1UL, 2UL);
            var result = await fixture.Query(@"mutation {
                update_links(where:{from:{to_id:{_eq:1}}},_set:{from_id:2,to_id:1}) {
                    affected_rows returning{id from_id to_id}
                }
            }");
            Assert.Equal(2, (int)result["update_links"]!["affected_rows"]!);
            // Native pair-identity behavior may merge identical updated pairs;
            // the selected-row count must nevertheless reflect both original IDs.
            Assert.All(result["update_links"]!["returning"]!, row =>
            {
                Assert.Equal(2, (long)row["from_id"]!);
                Assert.Equal(1, (long)row["to_id"]!);
            });
            var untouched = fixture.Links.GetLink(2UL);
            Assert.NotNull(untouched);
            Assert.Equal(2UL, untouched![1]);
            Assert.Equal(2UL, untouched[2]);
        }

        [Fact]
        public async Task LogicalSiblingsConstrainUpdateAndDeleteSelections()
        {
            using var fixture = new GraphFixture();
            var result = await fixture.Query(@"mutation {
                update_links(where:{_and:[],id:{_eq:3},from:{id:{_eq:2}}},_set:{from_id:4,to_id:4}) {affected_rows returning{id}}
            }");
            Assert.Equal(0, (int)result["update_links"]!["affected_rows"]!);
            result = await fixture.Query(@"mutation {
                delete_links(where:{_or:[{}],from:{id:{_eq:3}}}) {affected_rows returning{id}}
            }");
            Assert.Equal(1, (int)result["delete_links"]!["affected_rows"]!);
            Assert.Equal(new long[] { 4 }, Ids(result["delete_links"]!["returning"]!));
            Assert.Equal(new long[] { 1, 2, 3, 5, 6, 7 }, Ids((await fixture.Query("{links(order_by:{id:asc}){id}}"))["links"]!));
        }

        [Fact]
        public async Task DeepDeleteUsesTheGraphBeforeAnySelectedRowIsRemoved()
        {
            using var fixture = new GraphFixture();
            using var control = new GraphFixture();
            var result = await fixture.Query("mutation {delete_links(where:{from:{from:{id:{_eq:1}}}}){affected_rows returning{id}}}");
            Assert.Equal(new long[] { 1, 3, 4 }, Ids(result["delete_links"]!["returning"]!));
            // Keep the store's own referenced-link deletion behavior. The
            // GraphQL operation must select exactly these original IDs and
            // produce the same complete graph as applying the native deletes.
            foreach (var id in new ulong[] { 1, 3, 4 }) control.Links.Delete(id);
            const string all = "{links(order_by:{id:asc}){id from_id to_id}}";
            Assert.True(JToken.DeepEquals(await control.Query(all), await fixture.Query(all)));
        }

        [Theory]
        [InlineData("{_and:[null]}")]
        [InlineData("{_or:[null]}")]
        [InlineData("{_and:[{from:{_or:[null]}}]}")]
        [InlineData("{type:{id:{_eq:1}}}")]
        [InlineData("{type_id:{_eq:0}}")]
        public async Task InvalidOrUnsupportedPredicatesFailBeforeMutation(string where)
        {
            using var fixture = new GraphFixture();
            var result = await fixture.Execute("mutation {delete_links(where:" + where + "){affected_rows}}");
            Assert.NotEmpty(result["errors"]!);
            Assert.Equal(new long[] { 1, 2, 3, 4, 5, 6, 7 }, Ids((await fixture.Query("{links(order_by:{id:asc}){id}}"))["links"]!));
        }

        [Fact]
        public async Task ExcessiveInputNestingIsRejectedBeforeMutation()
        {
            using var fixture = new GraphFixture();
            var where = "{}";
            for (var i = 0; i < 34; i++) where = "{_not:" + where + "}";
            var result = await fixture.Execute("mutation {delete_links(where:" + where + "){affected_rows}}");
            Assert.Contains("nesting", result["errors"]!.ToString(), StringComparison.OrdinalIgnoreCase);
            Assert.Equal(new long[] { 1, 2, 3, 4, 5, 6, 7 }, Ids((await fixture.Query("{links(order_by:{id:asc}){id}}"))["links"]!));
        }

        private static long[] Ids(JToken rows) => rows.Select(row => (long)row["id"]!).ToArray();

        private sealed class GraphFixture : IDisposable
        {
            private readonly string _path = Path.Combine(Path.GetTempPath(), "doublets-deep-" + Guid.NewGuid() + ".links");
            private readonly LinksSchema _schema;
            private readonly ServiceProvider _services;
            public ILinks<ulong> Links { get; }

            public GraphFixture(bool seed = true)
            {
                Links = new UnitedMemoryLinks<ulong>(new FileMappedResizableDirectMemory(_path), UnitedMemoryLinks<ulong>.DefaultLinksSizeStep,
                    new LinksConstants<ulong>(true), IndexTreeType.Default);
                _services = new ServiceCollection().AddSingleton(Links).BuildServiceProvider();
                _schema = new LinksSchema(Links, new DefaultServiceProvider());
                if (seed)
                {
                    var pairs = new (ulong From, ulong To)[] { (1, 1), (2, 2), (1, 2), (3, 2), (4, 3), (2, 1), (0, 0) };
                    for (var i = 0; i < pairs.Length; i++)
                    {
                        Assert.Equal((ulong)i + 1, Links.GetOrCreate(pairs[i].From, pairs[i].To));
                    }
                }
            }

            public async Task<JObject> Execute(string query)
            {
                return JObject.Parse(await _schema.ExecuteAsync(options =>
                {
                    options.Query = query;
                    options.RequestServices = _services;
                }));
            }

            public async Task<JToken> Query(string query)
            {
                var response = await Execute(query);
                Assert.False(response.ContainsKey("errors"), response.ToString());
                return response["data"]!;
            }

            public void Dispose()
            {
                _schema.Dispose();
                _services.Dispose();
                (Links as IDisposable)?.Dispose();
                File.Delete(_path);
            }
        }
    }
}
