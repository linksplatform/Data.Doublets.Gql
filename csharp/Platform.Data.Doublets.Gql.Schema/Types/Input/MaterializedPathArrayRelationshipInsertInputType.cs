using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    /// <remarks>
    /// """
    /// input type for inserting array relation for remote table "mp"
    /// """
    /// input mp_arr_rel_insert_input {
    ///   data: [mp_insert_input!]!
    ///   on_conflict: mp_on_conflict
    /// }
    /// </remarks>
    public class MaterializedPathArrayRelationshipInsertInputType : ObjectGraphType<MaterializedPathArrayRelationshipInsert>
    {
        public MaterializedPathArrayRelationshipInsertInputType()
        {
            Name = "mp_arr_rel_insert_input";
            Description = "input type for inserting array relation for remote table \"mp\"";
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<MaterializedPathArrayRelationshipInsertInputType>>>>("data");
            Field(x => x.on_conflict, nullable: true, type: typeof(MaterializedPathOnConflictInputType));
        }
    }
}
