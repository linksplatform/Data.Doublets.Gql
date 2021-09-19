using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    /// <remarks>
    /// """
    /// input type for updating data in table "links"
    /// """
    /// input links_set_input {
    ///   from_id: bigint
    ///   id: bigint
    ///   to_id: bigint
    ///   type_id: bigint
    /// }
    /// </remarks>
    public class LinksSetInputType : InputObjectGraphType<LinksSet>
    {
        public LinksSetInputType()
        {
            Name = "links_set_input";
            Field(x => x.from_id, nullable: true, type: typeof(LongGraphType));
            Field(x => x.id, nullable: true, type: typeof(LongGraphType));
            Field(x => x.to_id, nullable: true, type: typeof(LongGraphType));
            Field(x => x.type_id, nullable: true, type: typeof(LongGraphType));
        }
    }
}