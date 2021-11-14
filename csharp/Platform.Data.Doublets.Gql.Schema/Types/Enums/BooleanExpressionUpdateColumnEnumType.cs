using Platform.Data.Doublets.Gql.Schema.Enums;

namespace Platform.Data.Doublets.Gql.Schema.Types.Enums
{
    public class BooleanExpressionUpdateColumnEnumType : BaseEnumType<BooleanExpressionUpdateColumn>
    {
        public BooleanExpressionUpdateColumnEnumType() : base("bool_exp_update_column",
            "update columns of table \"bool_exp\"")
        {
        }
    }
}