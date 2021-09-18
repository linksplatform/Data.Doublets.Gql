using GraphQL.Types;
using Platform.Data.Doublets.Gql.Schema.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types
{
    /// <remarks>
    /// """
    /// aggregated selection of "links"
    /// """
    /// type links_aggregate {
    ///   from_idaggregate: links_aggregate_fields
    ///   from_idnodes: [links!]!
    /// }
    /// </remarks>
    internal class LinksAggregateType : ObjectGraphType<LinksAggregate>
    {
        public LinksAggregateType()
        {
            Field(x => x.aggregate, nullable: true, type: typeof(LinksAggregateFieldsType));
            Field<ListGraphType<LinksType>>("nodes");
        }
    }
}
