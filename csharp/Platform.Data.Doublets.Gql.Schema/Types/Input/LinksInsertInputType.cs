using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    /// <remarks>
    ///     """
    ///     input type for inserting data into table "links"
    ///     """
    ///     input links_insert_input {
    ///     from: links_obj_rel_insert_input
    ///     from_id: bigint
    ///     id: bigint
    ///     in: links_arr_rel_insert_input
    ///     out: links_arr_rel_insert_input
    ///     to: links_obj_rel_insert_input
    ///     to_id: bigint
    ///     type: links_obj_rel_insert_input
    ///     type_id: bigint
    ///     }
    /// </remarks>
    public class LinksInsertInputType : InputObjectGraphType<LinksInsert>
    {
        public LinksInsertInputType()
        {
            Name = "links_insert_input";
            Field(x => x.from, true, typeof(LinksObjRelInsertInputType));
            Field(x => x.from_id, true, typeof(LongGraphType));
            Field(x => x.id, true, typeof(LongGraphType));
            Field(x => x.@in, true, typeof(LinksArrRelInsertInputType));
            Field(x => x.@out, true, typeof(LinksArrRelInsertInputType));
            Field(x => x.to, true, typeof(LinksObjRelInsertInputType));
            Field(x => x.to_id, true, typeof(LongGraphType));
            Field(x => x.type, true, typeof(LinksObjRelInsertInputType));
            Field(x => x.type_id, true, typeof(LongGraphType));
        }
    }
}
