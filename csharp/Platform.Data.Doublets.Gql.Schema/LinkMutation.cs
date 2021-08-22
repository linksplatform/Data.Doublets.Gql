using GraphQL;
using GraphQL.Types;
using Platform.Data.Doublets.Gql.Schema.Input;
using Platform.Data.Doublets.Gql.Schema.Types;
using System.Collections.Generic;
using System.Linq;

namespace Platform.Data.Doublets.Gql.Schema
{
    public class LinkMutation : ObjectGraphType<object>
    {
        public LinkMutation(ILinks<ulong> links)
        {
            Field<LinksMutationResponseType>("insert_links_one",
                arguments: new QueryArguments(
                    new QueryArgument<LinkInputType> { Name = "object" }
                ),
                resolve: context =>
                {
                    return new LinksMutationResponse()
                    {
                        returning = new List<Link>() { InsertLink(links, context.GetArgument<Link>("object")) },
                        affected_rows = 1
                    };
                });
            Field<LinksMutationResponseType>("insert_links",
                arguments: new QueryArguments(
                    new QueryArgument<ListGraphType<LinkInputType>> { Name = "objects" }
                ),
                resolve: context =>
                {
                    var response = new LinksMutationResponse() { returning = new List<Link>() };
                    foreach (var link in context.GetArgument<List<Link>>("objects"))
                    {
                        response.returning.Add(InsertLink(links, link));
                    }
                    response.affected_rows = response.returning.Count;
                    return response;
                });
            Field<LinksMutationResponseType>("delete_links",
                arguments: new QueryArguments(
                    new QueryArgument<LinkBooleanExpressionInputType> { Name = "where" }
                ),
                resolve: context =>
                {
                    var response = new LinksMutationResponse
                    {
                        returning = LinkQuery.GetLinks(context, links).ToList()
                    };
                    response.affected_rows = response.returning.Count();
                    foreach (var linkToDelete in response.returning)
                    {
                        links.Delete((ulong)linkToDelete.id);
                    }
                    return response;
                });
            Field<LinksMutationResponseType>("update_links",
                arguments: new QueryArguments(
                    new QueryArgument<LinkInputType> { Name = "_set" },
                    new QueryArgument<LinkBooleanExpressionInputType> { Name = "where" }
                ),
                resolve: context =>
                {
                    var set = context.GetArgument<Link>("_set");
                    var response = new LinksMutationResponse() { returning = new List<Link>() };
                    foreach (var link in LinkQuery.GetLinks(context, links))
                    {
                        var updatedLink = links.UpdateOrCreateOrGet((ulong)link.from_id, (ulong)link.to_id, (ulong)set.from_id, (ulong)set.to_id);
                        response.returning.Add(new Link(links.GetLink(updatedLink)));
                    }
                    response.affected_rows = response.returning.Count;
                    return response;
                });
        }
        public static Link InsertLink(object service, Link link)
        {
            var Links = (ILinks<ulong>)service;
            var create = Links.GetOrCreate((ulong)link.from_id, (ulong)link.to_id);
            return LinkType.GetLinkOrDefault(service, (long)create);
        }
    }
}
