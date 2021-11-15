using Platform.Data.Doublets.Gql.Schema.Enums;

namespace Platform.Data.Doublets.Gql.Schema.Types.Enums
{
    using MappedType = NumberConstraint;
    public class NumberConstraintEnumType : BaseEnumType<MappedType>
    {
        public NumberConstraintEnumType() : base("number_constraint", "unique or primary key constraints on table \"number\"")
        {
        }
    }
}
