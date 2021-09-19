using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types
{
    /// <remarks>
    /// """aggregate max on columns"""
    /// type links_max_fields {
    ///   from_id: bigint
    ///   id: bigint
    ///   to_id: bigint
    ///   type_id: bigint
    /// }
    /// </remarks>
    public class LinksAggregateBigIntMaxFieldsType : LinksAggregateBigIntFieldsType
    {
        public LinksAggregateBigIntMaxFieldsType()
        {
            Name = "links_max_fields";
            Field<LongGraphType>("id");
            Field<LongGraphType>("from_id");
            Field<LongGraphType>("to_id");
            Field<LongGraphType>("type_id");
        }
    }
}