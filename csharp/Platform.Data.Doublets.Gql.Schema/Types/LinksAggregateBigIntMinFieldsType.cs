using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types
{
    /// <remarks>
    /// """aggregate min on columns"""
    /// type links_min_fields {
    ///   from_id: bigint
    ///   id: bigint
    ///   to_id: bigint
    ///   type_id: bigint
    /// }
    /// </remarks>
    public class LinksAggregateBigIntMinFieldsType : LinksAggregateBigIntFieldsType
    {
        public LinksAggregateBigIntMinFieldsType() : base("links_min_fields") {}
    }
}
