using Platform.Data.Doublets.Gql.Schema.Enums;

namespace Platform.Data.Doublets.Gql.Schema.Types.Enums
{
    public class BooleanExpressionConstraintEnumType : BaseEnumType<BoolExpressionConstraint>
    {
        public BooleanExpressionConstraintEnumType() : base("bool_exp_constraint",
            "unique or primary key constraints on table \"bool_exp\"")
        {
        }
    }
}
