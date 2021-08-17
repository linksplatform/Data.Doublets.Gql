using System.Collections.Generic;
using GraphQL.NewtonsoftJson;
using GraphQL.Types;
using Platform.Data.Doublets;
using Input;
using GraphQL;

namespace Platform.Data.Doublets.Gql.Schema
{
    public class LinkMutation : ObjectGraphType<object>
    {
        public LinkMutation(ILinks links)
        {
            Field<LinkType>("insert_links_one",
                arguments: new QueryArguments(
                    new QueryArgument<LinkInputType> { Name = "object" }
                ),
                resolve: context =>
                {
                    var receivedLink = context.GetArgument<Link>("object");
                    var link = links.InsertLink(context.RequestServices.GetService(typeof(ILinks<ulong>)), receivedLink);
                    return link;
                });
            Field<ListGraphType<LinkType>>("insert_links",
                arguments: new QueryArguments(
                    new QueryArgument<ListGraphType<LinkInputType>> { Name = "objects" }
                ),
                resolve: context =>
                {
                   // EnumGraphType a;
                    var insertLinks = new List<Link>();
                    var receivedLinks = context.GetArgument<List<Link>>("objects");
                    var linksStorage = context.RequestServices.GetService(typeof(ILinks<ulong>));
                    foreach (var link in receivedLinks)
                    {
                        insertLinks.Add(links.InsertLink(linksStorage, link));
                    }
                    return insertLinks;
                });
        }
    }
}
