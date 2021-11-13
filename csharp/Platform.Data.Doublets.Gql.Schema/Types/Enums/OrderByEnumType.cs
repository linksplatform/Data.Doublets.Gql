using Platform.Data.Doublets.Gql.Schema.Enums;

namespace Platform.Data.Doublets.Gql.Schema.Types.Enums
{

    public class OrderByEnumType : BaseEnumType<OrderBy>
    {
        public OrderByEnumType() : base("order_by", "column ordering options")
        {
        }
    }
}
