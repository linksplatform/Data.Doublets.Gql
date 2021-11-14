using Platform.Data.Doublets.Gql.Schema.Enums;

namespace Platform.Data.Doublets.Gql.Schema.Types.Enums
{
    public class MaterializedPathUpdateColumnEnumType : BaseEnumType<MaterializedPathUpdateColumn>
    {
        public MaterializedPathUpdateColumnEnumType() : base("mp_update_column", "update columns of table \"mp\"")
        {

        }
    }
}
