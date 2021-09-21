using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types
{
    /// <remarks>
    /// """aggregate variance on columns"""
    /// type links_variance_fields {
    ///   from_id: Float
    ///   id: Float
    ///   to_id: Float
    ///   type_id: Float
    /// }
    /// </remarks>
    public class LinksAggregateFloatVarianceFieldsType : LinksAggregateFloatFieldsType
    {
        public LinksAggregateFloatVarianceFieldsType() : base("links_variance_fields") {}
    }
}
