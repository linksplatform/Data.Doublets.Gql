namespace Platform.Data.Doublets.Gql.Schema.Types.Enum
{
    public class NumberConstraintEnumType : BaseEnumType<NumberConstraint>
    {
        public NumberConstraintEnumType() : base("number_constraint",
            "unique or primary key constraints on table \"number\"")
        {
        }
    }
}
