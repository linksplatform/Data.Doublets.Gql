using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    using MappedType = BooleanExpressionObjectRelationshipInsert;

    public class BooleanExpressionObjectRelationshipInsertInputType : InputObjectGraphType<MappedType>
    {
        public BooleanExpressionObjectRelationshipInsertInputType()
        {
            Name = "bool_exp_obj_rel_insert_input";
            Field<NonNullGraphType<BooleanExpressionInsertInputType>>(nameof(BooleanExpressionObjectRelationshipInsert.data));
            Field<BooleanExpressionOnConflictInputType>(nameof(MappedType.on_conflict));
        }
    }
}
