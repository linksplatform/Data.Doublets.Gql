using GraphQL.Types;
using Platform.Data.Doublets.Gql.Schema.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types
{
    /// <remarks>
    /// """
    /// aggregated selection of "links"
    /// """
    /// type links_aggregate {
    ///   aggregate: links_aggregate_fields
    ///   nodes: [links!]!
    /// }
    /// </remarks>
    public class LinksAggregateType : ObjectGraphType<LinksAggregate>
    {
        public LinksAggregateType()
        {
            Name = "links_aggregate";
            Field(x => x.aggregate, nullable: true, type: typeof(LinksAggregateFieldsType));
            Field<ListGraphType<LinksType>>("nodes");
        }
    }
}
