using GraphQL;
using GraphQL.Types;
using Platform.Data.Doublets.Gql.Schema.Types;
using Platform.Data.Doublets.Gql.Schema.Types.Input;
using System.Collections.Generic;
using System.Linq;

namespace Platform.Data.Doublets.Gql.Schema
{
    public class LinksMutation : ObjectGraphType<object>
    {
        public LinksMutation(ILinks<ulong> links)
        {
            Name = "mutation_root";
            Field<LinksMutationResponseType>("delete_links", arguments: new QueryArguments(new QueryArgument<NonNullGraphType<LinksBooleanExpressionInputType>> { Name = "where" }), resolve: context =>
            {
                var response = new LinksMutationResponse { returning = LinksQuery.GetLinks(context, links).ToList() };
                response.affected_rows = response.returning.Count;
                foreach (var linkToDelete in response.returning)
                {
                    links.Delete((ulong)linkToDelete.id);
                }
                return response;
            });
            Field<LinksType>("delete_links_by_pk", arguments: new QueryArguments { new QueryArgument<NonNullGraphType<LongGraphType>> { Name = "id" } }, resolve: context => "");
            Field<LinksMutationResponseType>("insert_links", arguments: new QueryArguments(new QueryArgument<NonNullGraphType<ListGraphType<NonNullGraphType<LinksInsertInputType>>>> { Name = "objects" }, new QueryArgument<LinksOnConflictInputType> { Name = "on_conflict" }), resolve: context =>
            {
                var response = new LinksMutationResponse { returning = new List<Links>() };
                foreach (var link in context.GetArgument<List<LinksInsert>>("objects"))
                {
                    response.returning.Add(InsertLink(links, link));
                }
                response.affected_rows = response.returning.Count;
                return response;
            });
            Field<LinksType>("insert_links_one", arguments: new QueryArguments(new QueryArgument<NonNullGraphType<LinksInsertInputType>> { Name = "object" }, new QueryArgument<LinksOnConflictInputType> { Name = "on_conflict" }), resolve: context => InsertLink(links, context.GetArgument<LinksInsert>("object")));
            Field<LinksMutationResponseType>("update_links", arguments: new QueryArguments(new QueryArgument<LinksIncInputType> { Name = "_inc" }, new QueryArgument<LinksSetInputType> { Name = "_set" }, new QueryArgument<NonNullGraphType<LinksBooleanExpressionInputType>> { Name = "where" }), resolve: context =>
            {
                var set = context.GetArgument<LinksSet>("_set");
                var response = new LinksMutationResponse { returning = new List<Links>() };
                foreach (var link in LinksQuery.GetLinks(context, links))
                {
                    var updatedLink = links.Update((ulong)link.from_id.Value, (ulong)link.to_id.Value, (ulong)set.from_id.Value, (ulong)set.to_id.Value);
                    response.returning.Add(new Links(links.GetLink(updatedLink)));
                }
                response.affected_rows = response.returning.Count;
                return response;
            });
            Field<LinksType>("update_links_by_pk", arguments: new QueryArguments(new QueryArgument<LinksIncInputType> { Name = "_inc" }, new QueryArgument<LinksSetInputType> { Name = "_set" }, new QueryArgument<NonNullGraphType<LinksPkColumnsInputType>> { Name = "pk_columns" }), resolve: context => "");
        }

        public static Links InsertLink(object service, Links links)
        {
            var link = (ILinks<ulong>)service;
            var create = link.GetOrCreate((ulong)links.from_id, (ulong)links.to_id);
            return LinksType.GetLinkOrDefault(service, (long)create);
        }

        public static Links InsertLink(object service, LinksInsert linksInsert)
        {
            var link = (ILinks<ulong>)service;
            var create = link.GetOrCreate((ulong)linksInsert.from_id, (ulong)linksInsert.to_id);
            return LinksType.GetLinkOrDefault(service, (long)create);
        }
    }
}
