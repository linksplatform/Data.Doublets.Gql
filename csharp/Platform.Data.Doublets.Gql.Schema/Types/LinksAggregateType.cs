using GraphQL.Types;
using Platform.Data.Doublets.Gql.Schema.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types
{
    internal class LinksAggregateType : ObjectGraphType
    {
        public LinksAggregateType()
        {
            Field<LinksAggregateFieldsType>("aggregate", null, nullable: true, null);
            Field<ListGraphType<LinkType>>("nodes");
        }
    }
}
