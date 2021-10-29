using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    using MappedType = LinksInsert;
    /// <remarks>
    ///     """
    ///     input type for updating data in table "links"
    ///     """
    ///     input links_set_input {
    ///     from_id: bigint
    ///     id: bigint
    ///     to_id: bigint
    ///     type_id: bigint
    ///     }
    /// </remarks>
    public class LinksSetInputType : InputObjectGraphType<MappedType>
    {
        public LinksSetInputType()
        {
            Name = "links_set_input";
            Field<LongGraphType>(nameof(MappedType.from_id));
            Field<LongGraphType>(nameof(MappedType.id));
            Field<LongGraphType>(nameof(MappedType.to_id));
            Field<LongGraphType>(nameof(MappedType.type_id));
        }
    }
}
