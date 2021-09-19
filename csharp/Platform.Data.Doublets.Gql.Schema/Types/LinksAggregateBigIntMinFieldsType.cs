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
        public LinksAggregateBigIntMinFieldsType()
        {
            Name = "links_min_fields";
            Field<LongGraphType>("id");
            Field<LongGraphType>("from_id");
            Field<LongGraphType>("to_id");
            Field<LongGraphType>("type_id");
        }
    }
}