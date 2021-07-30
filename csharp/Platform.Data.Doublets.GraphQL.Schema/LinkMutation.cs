using GraphQL.NewtonsoftJson;
using GraphQL.Types;
using Platform.Data.Doublets;

namespace GraphQL.Samples.Schemas.Link
{
    public class LinkMutation : ObjectGraphType<object>
    {
        public LinkMutation(ILinks links)
        {
            Field<LinkType>("addLinks",
                arguments: new QueryArguments(
                    new QueryArgument<LinkInputType> { Name = "link" }
                ),
                resolve: context =>
                {
                    var receivedLink = context.GetArgument<Link>("link");
                    var link = links.insert_link(context.RequestServices.GetService(typeof(ILinks<ulong>)), receivedLink);
                    return link;
                });
        }
    }
}
