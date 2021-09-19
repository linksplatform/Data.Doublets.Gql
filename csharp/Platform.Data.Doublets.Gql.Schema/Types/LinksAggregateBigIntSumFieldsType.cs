using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types
{
    /// <remarks>
    /// """aggregate sum on columns"""
    /// type links_sum_fields {
    ///   from_id: bigint
    ///   id: bigint
    ///   to_id: bigint
    ///   type_id: bigint
    /// }
    /// </remarks>
    public class LinksAggregateBigIntSumFieldsType : LinksAggregateBigIntFieldsType
    {
        public LinksAggregateBigIntSumFieldsType()
        {
            Name = "links_sum_fields";
            Field<LongGraphType>("id");
            Field<LongGraphType>("from_id");
            Field<LongGraphType>("to_id");
            Field<LongGraphType>("type_id");
        }
    }
}