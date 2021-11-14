using Platform.Data.Doublets.Gql.Schema.Enums;

namespace Platform.Data.Doublets.Gql.Schema.Types.Enums
{
    public class MaterializedPathConstraintEnumType : BaseEnumType<MaterializedPathConstraint>
    {
        public MaterializedPathConstraintEnumType() : base("mp_constraint",
            "unique or primary key constraints on table \"mp\"")
        {

        }
    }
}
