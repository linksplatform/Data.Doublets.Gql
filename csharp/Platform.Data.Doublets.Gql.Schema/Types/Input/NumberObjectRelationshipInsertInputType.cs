using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    using MappedType = NumberObjectRelationshipInsert;
    public class NumberObjectRelationshipInsertInputType : InputObjectGraphType<MappedType>
    {
        public NumberObjectRelationshipInsertInputType()
        {
            Name = "number_obj_rel_insert_input";
            Field<NonNullGraphType<NumberInsertInputType>>(nameof(MappedType.data));
            Field(x => x.on_conflict, true, typeof(NumberOnConflictInputType));
        }
    }
}
