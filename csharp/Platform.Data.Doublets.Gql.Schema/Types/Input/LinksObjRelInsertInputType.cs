using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    using MappedType = LinksObjRelInsert;

    public class LinksObjRelInsertInputType : InputObjectGraphType<MappedType>
    {
        public LinksObjRelInsertInputType()
        {
            Name = "links_obj_rel_insert_input";
            Field<NonNullGraphType<LinksInsertInputType>>(nameof(MappedType.data));
            Field<LinksOnConflictInputType>(nameof(MappedType.on_conflict));
        }
    }
}
