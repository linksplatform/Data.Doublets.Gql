using Platform.Data.Doublets.Gql.Schema.Enums;

namespace Platform.Data.Doublets.Gql.Schema.Types.Enums
{
    using MappedType = MaterializedPathSelectColumn;

    public class MaterializedPathSelectColumnEnumType : BaseEnumType<MappedType>
    {
        public MaterializedPathSelectColumnEnumType() : base("mp_select_column", "select columns of table \"mp\"")
        {
        }
    }
}
