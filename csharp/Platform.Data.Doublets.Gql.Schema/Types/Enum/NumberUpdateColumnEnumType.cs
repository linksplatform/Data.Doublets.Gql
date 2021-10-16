using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Enum
{
    public class NumberUpdateColumnEnumType : BaseEnumType<NumberUpdateColumn>
    {
        public NumberUpdateColumnEnumType() : base("number_update_column", "update columns of table \"number\"")
        {

        }
    }
}
