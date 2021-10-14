using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    using MappedType = NumberObjectRelationshipInsert;
    /// <remarks>
    /// """
    /// input type for inserting object relation for remote table "number"
    /// """
    /// input number_obj_rel_insert_input {
    ///   data: number_insert_input!
    ///   on_conflict: number_on_conflict
    /// }
    /// </remarks>
    public class NumberObjectRelationshipInsertInputType : InputObjectGraphType<MappedType>
    {
        public NumberObjectRelationshipInsertInputType()
        {
            Name = "number_obj_rel_insert_input";
            Field<NumberInsertInputType>(nameof(MappedType.data));
            Field(x => x.on_conflict, nullable: true, type: typeof(NumberOnConflictInputType));
        }
    }
}
