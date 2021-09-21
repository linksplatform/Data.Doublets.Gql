using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    /// <remarks>
    ///     """
    ///     input type for incrementing integer column in table "links"
    ///     """
    ///     input links_inc_input {
    ///     from_id: bigint
    ///     id: bigint
    ///     to_id: bigint
    ///     type_id: bigint
    ///     }
    /// </remarks>
    public class LinksIncInputType : InputObjectGraphType<Links>
    {
        public LinksIncInputType()
        {
            Name = "links_inc_input";
            Field(x => x.id, true, typeof(LongGraphType));
            Field(x => x.from_id, true, typeof(LongGraphType));
            Field(x => x.to_id, true, typeof(LongGraphType));
            Field(x => x.type_id, true, typeof(LongGraphType));
        }
    }
}
