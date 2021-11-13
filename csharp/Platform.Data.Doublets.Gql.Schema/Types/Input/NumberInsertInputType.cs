using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    /// <remarks>
    ///     """
    ///     input type for inserting data into table "number"
    ///     """
    ///     input number_insert_input {
    ///     id: bigint
    ///     link: links_arr_rel_insert_input
    ///     link_id: bigint
    ///     value: float8
    ///     }
    /// </remarks>
    public class NumberInsertInputType : InputObjectGraphType<NumberInsert>
    {
        public NumberInsertInputType()
        {
            Name = "number_insert_input";
            Field(x => x.id, true, typeof(LongGraphType));
            Field(x => x.link, true, typeof(LinksArrayRelationshipInsertInputType));
            Field(x => x.link_id, true, typeof(LongGraphType));
            Field(x => x.value, true, typeof(FloatGraphType));
        }
    }
}
