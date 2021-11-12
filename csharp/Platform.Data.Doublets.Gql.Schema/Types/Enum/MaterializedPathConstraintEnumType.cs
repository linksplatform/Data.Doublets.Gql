namespace Platform.Data.Doublets.Gql.Schema.Types.Enum
{
    public class MaterializedPathConstraintEnumType : BaseEnumType<MaterializedPathConstraint>
    {
        public MaterializedPathConstraintEnumType() : base("mp_constraint",
            "unique or primary key constraints on table \"mp\"")
        {
        }
    }
}