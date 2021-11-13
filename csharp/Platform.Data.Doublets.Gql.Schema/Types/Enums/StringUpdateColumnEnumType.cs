using Platform.Data.Doublets.Gql.Schema.Enums;

namespace Platform.Data.Doublets.Gql.Schema.Types.Enums
{
    public class StringUpdateColumnEnumType : BaseEnumType<StringUpdateColumn>
    {
        public StringUpdateColumnEnumType() : base("string_update_column", "update columns of table \"string\"")
        {
        }
    }
}
