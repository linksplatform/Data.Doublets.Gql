namespace Platform.Data.Doublets.Gql.Schema.Types
{
    /// <remarks>
    ///     """aggregate var_pop on columns"""
    ///     type links_var_pop_fields {
    ///     from_id: Float
    ///     id: Float
    ///     to_id: Float
    ///     type_id: Float
    ///     }
    /// </remarks>
    public class LinksAggregateFloatVarPopFieldsType : LinksAggregateFloatFieldsType
    {
        public LinksAggregateFloatVarPopFieldsType() : base("links_var_pop_fields")
        {
        }
    }
}