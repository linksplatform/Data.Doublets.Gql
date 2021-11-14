using Platform.Data.Doublets.Gql.Schema.Enums;

namespace Platform.Data.Doublets.Gql.Schema.Types.Enums
{
    public class NumberConstraintEnumType : BaseEnumType<NumberConstraint>
    {
        public NumberConstraintEnumType() : base("number_constraint",
            "unique or primary key constraints on table \"number\"")
        {
        }
    }
}