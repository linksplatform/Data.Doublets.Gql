using GraphQL.Types;
using Platform.Data.Doublets.Gql.Schema.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Data.Doublets.Gql.Schema
{
    class LinksSubscription : ObjectGraphType
    {
        public LinksSubscription(ILinks<ulong> links) => Field<ListGraphType<LinkType>>("links",
        arguments: LinkQuery.Arguments,
        resolve: context =>
        {
            return LinkQuery.GetLinks(context, links);
        });
    }
}
