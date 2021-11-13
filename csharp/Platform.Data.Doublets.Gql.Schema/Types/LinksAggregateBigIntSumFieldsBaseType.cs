namespace Platform.Data.Doublets.Gql.Schema.Types
{
    /// <remarks>
    ///     """aggregate sum on columns"""
    ///     type links_sum_fields {
    ///     from_id: bigint
    ///     id: bigint
    ///     to_id: bigint
    ///     type_id: bigint
    ///     }
    /// </remarks>
    public class LinksAggregateBigIntSumFieldsBaseType : LinksAggregateBigIntFieldsBaseType
    {
        public LinksAggregateBigIntSumFieldsBaseType() : base("links_sum_fields")
        {
        }
    }
}
