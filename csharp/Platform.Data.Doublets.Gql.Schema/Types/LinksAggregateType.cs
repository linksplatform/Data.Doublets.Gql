using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types
{
    /// <remarks>
    ///     """
    ///     aggregated selection of "links"
    ///     """
    ///     type links_aggregate {
    ///     aggregate: links_aggregate_fields
    ///     nodes: [links!]!
    ///     }
    /// </remarks>
    public class LinksAggregateType : ObjectGraphType<LinksAggregate>
    {
        public LinksAggregateType()
        {
            Name = "links_aggregate";
            Field(x => x.aggregate, true, typeof(LinksAggregateFieldsType));
            Field<NonNullGraphType<ListGraphType<LinksType>>>("nodes");
        }
    }
}
