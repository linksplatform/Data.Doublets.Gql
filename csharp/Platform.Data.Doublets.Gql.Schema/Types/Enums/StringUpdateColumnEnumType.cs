using Platform.Data.Doublets.Gql.Schema.Enums;

namespace Platform.Data.Doublets.Gql.Schema.Types.Enums
{
    using MappedType = StringUpdateColumn;
    public class StringUpdateColumnEnumType : BaseEnumType<MappedType>
    {
        public StringUpdateColumnEnumType() : base("string_update_column", "update columns of table \"string\"")
        {
        }
    }
}
