using Platform.Data.Doublets.Gql.Schema.Enums;

namespace Platform.Data.Doublets.Gql.Schema.Types.Enums
{
    using MappedType = BooleanExpressionUpdateColumn;

    public class BooleanExpressionUpdateColumnEnumType : BaseEnumType<MappedType>
    {
        public BooleanExpressionUpdateColumnEnumType() : base("bool_exp_update_column", "update columns of table \"bool_exp\"") { }
    }
}
