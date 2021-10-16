using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Enum
{
    public class StringUpdateColumnEnumType : BaseEnumType<StringUpdateColumn>
    {
        public StringUpdateColumnEnumType() : base("string_update_column", "update columns of table \"string\"")
        {

        }
    }
}
