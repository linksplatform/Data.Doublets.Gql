using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types
{
    /// <remarks>
    /// """aggregate stddev on columns"""
    /// type links_stddev_fields {
    ///   from_id: Float
    ///   id: Float
    ///   to_id: Float
    ///   type_id: Float
    /// }
    /// </remarks>
    public class LinksAggregateFloatStddevFieldsType : LinksAggregateFloatFieldsType
    {
        public LinksAggregateFloatStddevFieldsType() : base("links_stddev_fields") {}
    }
}
