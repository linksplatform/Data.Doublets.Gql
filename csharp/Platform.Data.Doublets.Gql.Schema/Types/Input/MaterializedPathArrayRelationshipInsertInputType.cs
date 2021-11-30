using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    using MappedType = MaterializedPathArrayRelationshipInsert;

    public class MaterializedPathArrayRelationshipInsertInputType : InputObjectGraphType<MappedType>
    {
        public MaterializedPathArrayRelationshipInsertInputType()
        {
            Name = "mp_arr_rel_insert_input";
            Description = "input type for inserting array relation for remote table \"mp\"";
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<MaterializedPathInsertInputType>>>>("data");
            Field<MaterializedPathOnConflictInputType>(nameof(MappedType.on_conflict));
        }
    }
}
