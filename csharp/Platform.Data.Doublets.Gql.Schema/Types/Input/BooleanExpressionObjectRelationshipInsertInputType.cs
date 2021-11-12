using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    /// <remarks>
    ///     """
    ///     input type for inserting object relation for remote table "bool_exp"
    ///     """
    ///     input bool_exp_obj_rel_insert_input {
    ///     data: bool_exp_insert_input!
    ///     on_conflict: bool_exp_on_conflict
    ///     }
    /// </remarks>
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
