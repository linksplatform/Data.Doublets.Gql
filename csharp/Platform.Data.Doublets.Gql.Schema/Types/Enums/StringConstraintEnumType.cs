using Platform.Data.Doublets.Gql.Schema.Enums;

namespace Platform.Data.Doublets.Gql.Schema.Types.Enums
{
    using MappedType = StringConstraint;

    public class StringConstraintEnumType : BaseEnumType<MappedType>
    {
        public StringConstraintEnumType() : base("string_constraint", "unique or primary key constraints on table \"string\"")
        {
        }
    }
}
