using Platform.Data.Doublets.Gql.Schema.Enums;

namespace Platform.Data.Doublets.Gql.Schema.Types.Enums
{
    using MappedType = BoolExpressionConstraint;
    public class BooleanExpressionConstraintEnumType : BaseEnumType<MappedType>
    {
        public BooleanExpressionConstraintEnumType() : base("bool_exp_constraint",
            "unique or primary key constraints on table \"bool_exp\"")
        {
        }
    }
}
