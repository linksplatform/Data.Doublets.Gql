using Platform.Data.Doublets.Gql.Schema.Enums;

namespace Platform.Data.Doublets.Gql.Schema.Types.Enums
{
    using MappedType = MaterializedPathUpdateColumn;
    public class MaterializedPathUpdateColumnEnumType : BaseEnumType<MappedType>
    {
        public MaterializedPathUpdateColumnEnumType() : base("mp_update_column", "update columns of table \"mp\"")
        {
        }
    }
}
