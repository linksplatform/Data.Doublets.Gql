using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema
{
    internal class OrderByEnumType : EnumerationGraphType<order_by>
    {
        public OrderByEnumType() => Name = "OrderByEnum";
    }
}
