using GraphQL.Types;
using Platform.Data.Doublets.Gql.Schema.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types
{
    internal class LinksAggregateType : ObjectGraphType<LinksAggregate>
    {
        public LinksAggregateType()
        {
            Field(x => x.aggregate, nullable: true, type: typeof(LinksAggregateFieldsType));
            Field<ListGraphType<LinkType>>("nodes");
        }
    }
}
