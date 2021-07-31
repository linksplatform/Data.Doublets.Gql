using GraphQL.NewtonsoftJson;
using GraphQL.Types;
using Platform.Data.Doublets;

namespace GraphQL.Samples.Schemas.Link
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
        }
    }
}
