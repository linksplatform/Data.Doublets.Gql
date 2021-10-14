using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    using MappedType = StringObjectRelationshipInsert;
    /// <remarks>
    /// """
    /// input type for inserting object relation for remote table "string"
    /// """
    /// input string_obj_rel_insert_input {
    ///   data: string_insert_input!
    ///   on_conflict: string_on_conflict
    /// }
    /// </remarks>
    public class StringObjectRelationshipInsertInputType : InputObjectGraphType<MappedType>
    {
        public StringObjectRelationshipInsertInputType()
        {
            Name = "String_obj_rel_insert_input";
            Field<StringInsertInputType>(nameof(MappedType.data));
            Field(x => x.on_conflict, nullable: true, type: typeof(StringOnConflictInputType));
        }
    }
}