namespace Platform.Data.Doublets.Gql.Schema.Types
{
    /// <remarks>
    ///     """aggregate var_samp on columns"""
    ///     type links_var_samp_fields {
    ///     from_id: Float
    ///     id: Float
    ///     to_id: Float
    ///     type_id: Float
    ///     }
    /// </remarks>
    public class LinksAggregateFloatVarSampFieldsType : LinksAggregateFloatFieldsType
    {
        public LinksAggregateFloatVarSampFieldsType() : base("links_var_samp_fields")
        {
        }
    }
}
