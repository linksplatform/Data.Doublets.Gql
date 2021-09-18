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
    ///
    /// """aggregate stddev on columns"""
    /// type links_stddev_fields {
    ///   from_id: Float
    ///   id: Float
    ///   to_id: Float
    ///   type_id: Float
    /// }
    ///
    /// """aggregate stddev_pop on columns"""
    /// type links_stddev_pop_fields {
    ///   from_id: Float
    ///   id: Float
    ///   to_id: Float
    ///   type_id: Float
    /// }
    ///
    /// """aggregate stddev_samp on columns"""
    /// type links_stddev_samp_fields {
    ///   from_id: Float
    ///   id: Float
    ///   to_id: Float
    ///   type_id: Float
    /// }
    ///
    /// """aggregate var_pop on columns"""
    /// type links_var_pop_fields {
    ///   from_id: Float
    ///   id: Float
    ///   to_id: Float
    ///   type_id: Float
    /// }
    ///
    /// """aggregate var_samp on columns"""
    /// type links_var_samp_fields {
    ///   from_id: Float
    ///   id: Float
    ///   to_id: Float
    ///   type_id: Float
    /// }
    ///
    /// """aggregate variance on columns"""
    /// type links_variance_fields {
    ///   from_id: Float
    ///   id: Float
    ///   to_id: Float
    ///   type_id: Float
    /// }
    /// </remarks>
    internal class LinksAggregateFloatFieldsType : ObjectGraphType<LinksAggregateFloatFields>
    {
        public LinksAggregateFloatFieldsType()
        {
            Field(x => x.id, nullable: true, type: typeof(FloatGraphType));
            Field(x => x.from_id, nullable: true, type: typeof(FloatGraphType));
            Field(x => x.to_id, nullable: true, type: typeof(FloatGraphType));
            Field(x => x.type_id, nullable: true, type: typeof(FloatGraphType));
        }
    }
}
