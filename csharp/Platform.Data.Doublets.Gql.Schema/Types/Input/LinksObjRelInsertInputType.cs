using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    using MappedType = LinksObjRelInsert;

    /// <remarks>
    ///     """
    ///     input type for inserting object relation for remote table "links"
    ///     """
    ///     input links_obj_rel_insert_input {
    ///     data: links_insert_input!
    ///     on_conflict: links_on_conflict
    ///     }
    /// </remarks>
    public class LinksObjRelInsertInputType : InputObjectGraphType<MappedType>
    {
        public LinksObjRelInsertInputType()
        {
            Name = "links_obj_rel_insert_input";
            Field<LinksInsertInputType>(nameof(MappedType.data));
            Field<LinksOnConflictInputType>(nameof(MappedType.on_conflict));
        }
    }
}
