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
    public class LinksAggregateBigIntMaxFieldsType : LinksAggregateBigIntFieldsType
    {
        public LinksAggregateBigIntMaxFieldsType() : base("links_max_fields")
        {
        }
    }
}
