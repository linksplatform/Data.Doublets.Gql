using GraphQL.Types;
using Platform.Data.Doublets.Gql.Schema.Types;

namespace Platform.Data.Doublets.Gql.Schema
{

    public class LinksSubscription : ObjectGraphType
    {
        public LinksSubscription(ILinks<ulong> links)
        {
            Name = "subscription_root";
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<LinksType>>>>("links",
                arguments: LinksQuery.Arguments,
                resolve: context => { return LinksQuery.GetLinks(context, links); });
            Field<NonNullGraphType<LinksAggregateType>>("links_aggregate",
                arguments: LinksQuery.Arguments,
                resolve: context => ""
            );
            Field<LinksType>("links_by_pk",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<LongGraphType>> { Name = "id" }
                )
            );
        }
    }
}
