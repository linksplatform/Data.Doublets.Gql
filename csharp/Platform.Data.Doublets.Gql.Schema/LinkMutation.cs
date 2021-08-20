using System.Collections.Generic;
using GraphQL.NewtonsoftJson;
using GraphQL.Types;
using Platform.Data.Doublets;
using Input;
using GraphQL;
using System;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using GraphQL.Types;
using GraphQL.Validation;
using Microsoft.Extensions.DependencyInjection;
using Platform.Data.Doublets;
using Input;
using static Platform.Data.Doublets.Gql.Schema.Link;
using GraphQL;
using Platform.Data;
using Platform.Data.Doublets;
using Platform.Data.Doublets.Memory;
using Platform.Data.Doublets.Memory.United.Generic;
using Platform.Memory;


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
            Field<ListGraphType<LinkType>>("delete_links",
                arguments: new QueryArguments(
                    new QueryArgument<LinkBooleanExpressionInputType> { Name = "where" }
                ),
                resolve: context =>
                {
                    var links = context.RequestServices.GetService<ILinks<ulong>>();
                    var any = links.Constants.Any;
                    var where = context.GetArgument<LinkBooleanExpression>("where");
                    var query = new Link<UInt64>(index: any, source: (ulong?)where?.from_id?._eq ?? any, target: (ulong?)where?.to_id?._eq ?? any);
                    var linksToDelete = new List<List<ulong>>();
                    links.Each(link =>
                    {
                        linksToDelete.Add((List<ulong>)link);
                        return links.Constants.Continue;
                    }, query);
                    foreach(var linkToDelete in linksToDelete)
                    {
                        links.Delete(linkToDelete);
                    }
                    List<Link> linksAfterDelete = new();
                    links.Each(link =>
                    {
                        linksAfterDelete.Add(new Link(link));
                        return links.Constants.Continue;
                    }, query);
                    return linksAfterDelete;
                });
        }
    }
}
