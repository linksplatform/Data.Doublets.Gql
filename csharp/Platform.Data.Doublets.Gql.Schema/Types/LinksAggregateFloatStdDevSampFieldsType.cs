using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types
{
    /// <remarks>
    /// """aggregate stddev_samp on columns"""
    /// type links_stddev_samp_fields {
    ///   from_id: Float
    ///   id: Float
    ///   to_id: Float
    ///   type_id: Float
    /// }
    /// </remarks>
    public class LinksAggregateFloatStdDevSampFieldsType : LinksAggregateFloatFieldsType
    {
        public LinksAggregateFloatStdDevSampFieldsType() : base("links_stddev_samp_fields") {}
    }
}
