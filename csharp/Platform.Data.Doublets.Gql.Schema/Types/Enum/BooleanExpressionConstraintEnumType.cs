using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Enum
{
    public class BooleanExpressionConstraintEnumType : BaseEnumType<BoolExpressionConstraint>
    {
        public BooleanExpressionConstraintEnumType() : base("bool_exp_constraint", "unique or primary key constraints on table \"bool_exp\"")
        {

        }
    }
}
