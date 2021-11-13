namespace Platform.Data.Doublets.Gql.Schema.Types
{
    /// <remarks>
    ///     """aggregate max on columns"""
    ///     type links_max_fields {
    ///     from_id: bigint
    ///     id: bigint
    ///     to_id: bigint
    ///     type_id: bigint
    ///     }
    /// </remarks>
    public class LinksAggregateBigIntMaxFieldsBaseType : LinksAggregateBigIntFieldsBaseType
    {
        public LinksAggregateBigIntMaxFieldsBaseType() : base("links_max_fields")
        {
        }
    }
}
