using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types
{
    /// <remarks>
    /// """aggregate avg on columns"""
    /// type links_avg_fields {
    ///   from_id: Float
    ///   id: Float
    ///   to_id: Float
    ///   type_id: Float
    /// }
    /// </remarks>
    public class LinksAggregateFloatAvgFieldsType : LinksAggregateFloatFieldsType
    {
        public LinksAggregateFloatAvgFieldsType() : base("links_avg_fields") {}
    }
}
