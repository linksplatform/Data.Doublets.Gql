using Platform.Data.Doublets.Gql.Schema.Enums;

namespace Platform.Data.Doublets.Gql.Schema.Types.Enums
{
    using MappedType = OrderBy;

    public class OrderByEnumType : BaseEnumType<MappedType>
    {
        public OrderByEnumType() : base("order_by", "column ordering options")
        {
        }
    }
}
