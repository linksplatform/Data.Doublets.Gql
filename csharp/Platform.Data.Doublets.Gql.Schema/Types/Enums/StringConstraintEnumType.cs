using Platform.Data.Doublets.Gql.Schema.Enums;

namespace Platform.Data.Doublets.Gql.Schema.Types.Enums
{
    public class StringConstraintEnumType : BaseEnumType<StringConstraint>
    {
        public StringConstraintEnumType() : base("string_constraint", "unique or primary key constraints on table \"string\"")
        {

        }
    }
}
