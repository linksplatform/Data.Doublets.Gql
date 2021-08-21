using System.Collections.Generic;
using GraphQL.NewtonsoftJson;
using GraphQL.Types;
using Platform.Data.Doublets;
using Input;
using GraphQL;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Platform.Data.Doublets.Gql.Schema
{
    public class LinkMutation : ObjectGraphType<object>
    {
        public LinkMutation(ILinks links)
        {
            Field<LinksMutationResponseType>("insert_links_one",
                arguments: new QueryArguments(
                    new QueryArgument<LinkInputType> { Name = "object" }
                ),
                resolve: context =>
                {
                    return new LinksMutationResponse()
                    {
                        returning = new List<Link>() { links.InsertLink(context.RequestServices.GetService(typeof(ILinks<ulong>)), context.GetArgument<Link>("object")) },
                        affected_rows = 1
                    };
                });
            Field<LinksMutationResponseType>("insert_links",
                arguments: new QueryArguments(
                    new QueryArgument<ListGraphType<LinkInputType>> { Name = "objects" }
                ),
                resolve: context =>
                {
                    var response = new LinksMutationResponse();
                    var linksStorage = context.RequestServices.GetService(typeof(ILinks<ulong>));
                    foreach (var link in context.GetArgument<List<Link>>("objects"))
                    {
                        response.returning.Add(links.InsertLink(linksStorage, link));
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
                    var links = context.RequestServices.GetService<ILinks<ulong>>();
                    var response = new LinksMutationResponse
                    {
                        returning = (List<Link>)LinkQuery.GetLinks(context, links)
                    };
                    response.affected_rows = response.returning.Count();
                    foreach(var linkToDelete in response.returning)
                    {
                        links.Delete((ulong)linkToDelete.id);
                    }
                    return response;
                });
        }
    }
}
