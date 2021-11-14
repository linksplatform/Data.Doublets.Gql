using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    public class
        BooleanExpressionObjectRelationshipInsertInputType : InputObjectGraphType<
            BooleanExpressionObjectRelationshipInsert>
    {
        public BooleanExpressionObjectRelationshipInsertInputType()
        {
            Name = "bool_exp_obj_rel_insert_input";
            Field<NonNullGraphType<BooleanExpressionInsertInputType>>(nameof(BooleanExpressionObjectRelationshipInsert
                .data));
            Field(x => x.on_conflict, true, typeof(BooleanExpressionOnConflictInputType));
        }
    }
}
