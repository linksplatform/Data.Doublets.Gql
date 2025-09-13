using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    using MappedType = StringObjectRelationshipInsert;

    public class StringObjectRelationshipInsertInputType : InputObjectGraphType<MappedType>
    {
        public StringObjectRelationshipInsertInputType()
        {
            Name = "string_obj_rel_insert_input";
            Field<StringInsertInputType>(nameof(MappedType.data));
            Field<StringOnConflictInputType>(nameof(MappedType.on_conflict));
            Field<StringGraphType>(nameof(MappedType.value));
        }
    }
}
