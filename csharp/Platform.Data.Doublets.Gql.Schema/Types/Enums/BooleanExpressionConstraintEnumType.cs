namespace Platform.Data.Doublets.Gql.Schema.Types.Enums
{
    /// <remarks>
    ///     """
    ///     unique or primary key constraints on table "bool_exp"
    ///     """
    ///     enum bool_exp_constraint {
    ///     """unique or primary key constraint"""
    ///     bool_exp_pkey
    ///     }
    /// </remarks>
    public class BooleanExpressionConstraintEnumType : BaseEnumType<BoolExpressionConstraint>
    {
        public BooleanExpressionConstraintEnumType() : base("bool_exp_constraint",
            "unique or primary key constraints on table \"bool_exp\"")
        {
        }
    }
}
