using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    using MappedType = StringObjectRelationshipInsert;

    public class StringObjectRelationshipInsertInputType : InputObjectGraphType<MappedType>
    {
        public StringObjectRelationshipInsertInputType()
        {
            Name = "string_obj_rel_insert_input";
            Field<NonNullGraphType<StringInsertInputType>>(nameof(MappedType.data));
            Field(x => x.on_conflict, true, typeof(StringOnConflictInputType));
        }
    }
}
