using System.Collections.Generic;
using System.Linq;
using GraphQL.Types;
using Platform.Data.Doublets;
using Platform.Data;
using Platform.Data.Doublets.Gql.Schema;
using GraphQL;

namespace Input
{
    public class LinkType : ObjectGraphType<Link>
    {
        public LinkType()
        {
            Field(o => o.id);
            Field(o => o.from_id);
            Field(o => o.from, false, type: typeof(LinkType)).Resolve(ResolveFrom);
            Field(o => o.to, type: typeof(LinkType)).Resolve(ResolveTo);
            Field(o => o.to_id);
            Field(o => o.type, type: typeof(LinkType)).Resolve(ResolveType);
            Field(o => o.type_id);
            QueryArguments args = new(
                 new QueryArgument<LongGraphType> { Name = "limit" },
                 new QueryArgument<LinkBooleanExpressionInputType> { Name = "where" },
                 new QueryArgument<OrderByInputType> { Name = "order_by" },
                 new QueryArgument<LongGraphType> { Name = "offset" },
                 new QueryArgument<ListGraphType<DistinctEnum>> { Name = "distinct" }
            );
            Field<ListGraphType<LinkType>>("in", null,args, ResolveIn, null); 
            Field<ListGraphType<LinkType>>("out", null,args, ResolveOut, null);
        }

        private List<Link> ResolveIn(IResolveFieldContext<Link> context) => LinkQuery.GetLinks(context, (ILinks<ulong>)context.RequestServices.GetService(typeof(ILinks<ulong>)), null, context.Source.id).ToList();
        
        private List<Link> ResolveOut(IResolveFieldContext<Link> context) => LinkQuery.GetLinks(context, (ILinks<ulong>)context.RequestServices.GetService(typeof(ILinks<ulong>)), context.Source.id, null).ToList();

        private Link ResolveFrom(IResolveFieldContext<Link> context) => context.Source.@from ?? GetLinkOrDefault(context, context.Source.from_id);

        private Link ResolveTo(IResolveFieldContext<Link> context) => context.Source.to ?? GetLinkOrDefault(context, context.Source.to_id);

        private Link ResolveType(IResolveFieldContext<Link> context) => context.Source.type ?? GetLinkOrDefault(context,context.Source.type_id);

        public static Link GetLinkOrDefault(IResolveFieldContext<Link> context, long linkId) => GetLinkOrDefault(context.RequestServices.GetService(typeof(ILinks<ulong>))
            , (linkId));
      
        public static Link GetLinkOrDefault(object service, long linkId) => GetLinkOrDefault((ILinks<ulong>)service, (ulong)linkId);

        public static Link GetLinkOrDefault(ILinks<ulong> links, ulong link) => links.Exists(link) ? new Link(links.GetLink(link)) :  null;
    }
}
