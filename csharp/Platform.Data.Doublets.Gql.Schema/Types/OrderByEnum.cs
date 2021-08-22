using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema
{
    internal class OrderByEnum : EnumerationGraphType<order_by>
    {
        public OrderByEnum() => Name = "OrderByEnum";
    }
}
