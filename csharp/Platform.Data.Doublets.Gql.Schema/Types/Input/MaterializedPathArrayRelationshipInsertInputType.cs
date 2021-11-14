using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    public class
        MaterializedPathArrayRelationshipInsertInputType : InputObjectGraphType<MaterializedPathArrayRelationshipInsert>
    {
        public MaterializedPathArrayRelationshipInsertInputType()
        {
            Name = "mp_arr_rel_insert_input";
            Description = "input type for inserting array relation for remote table \"mp\"";
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<MaterializedPathInsertInputType>>>>("data");
            Field(x => x.on_conflict, true, typeof(MaterializedPathOnConflictInputType));
        }
    }
}