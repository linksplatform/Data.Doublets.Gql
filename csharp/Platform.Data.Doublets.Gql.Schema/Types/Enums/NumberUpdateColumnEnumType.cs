using Platform.Data.Doublets.Gql.Schema.Enums;

namespace Platform.Data.Doublets.Gql.Schema.Types.Enums
{
    using MappedType = NumberUpdateColumn;

    public class NumberUpdateColumnEnumType : BaseEnumType<MappedType>
    {
        public NumberUpdateColumnEnumType() : base("number_update_column", "update columns of table \"number\"")
        {
        }
    }
}
