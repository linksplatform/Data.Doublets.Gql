using Platform.Data.Doublets.Gql.Schema.Enums;

namespace Platform.Data.Doublets.Gql.Schema.Types.Enums
{
    using MappedType = MaterializedPathConstraint;
    public class MaterializedPathConstraintEnumType : BaseEnumType<MappedType>
    {
        public MaterializedPathConstraintEnumType() : base("mp_constraint",
            "unique or primary key constraints on table \"mp\"")
        {
        }
    }
}
