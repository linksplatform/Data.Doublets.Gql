namespace Platform.Data.Doublets.Gql.Schema.Types.Enum
{
    public class StringConstraintEnumType : BaseEnumType<StringConstraint>
    {
        public StringConstraintEnumType() : base("string_constraint",
            "unique or primary key constraints on table \"string\"")
        {
        }
    }
}
