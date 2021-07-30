using GraphQL.NewtonsoftJson;
using GraphQL.Types;

namespace GraphQL.Samples.Schemas.Link
{
    public class LinkMutation : ObjectGraphType<object>
    {
        public LinkMutation(ILinks links)
        {
            Field<LinkType>("addLinks",
                arguments: new QueryArguments(
                    new QueryArgument<MessageInputType> { Name = "link" }
                ),
                resolve: context =>
                {
                    var receivedLink = context.GetArgument<Link>("link");
                    var link = links.insert_link(receivedLink);
                    return link;
                });
        }
    }
    

    public class MessageInputType : InputObjectGraphType
    {

    }
}
