using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    /// <remarks>
    /// """
    /// input type for inserting object relation for remote table "links"
    /// """
    /// input links_obj_rel_insert_input {
    ///   data: links_insert_input!
    ///   on_conflict: links_on_conflict
    /// }
    /// </remarks>
public class LinksObjRelInsertInputType : InputObjectGraphType<LinksObjRelInsert>
    {
        public LinksObjRelInsertInputType()
        {
            Name = "links_obj_rel_insert_input";
            Field<LinksInsertInputType>("data");
            Field(x => x.on_conflict, nullable: true, type: typeof(LinksOnConflictInputType));
        }
    }
}
