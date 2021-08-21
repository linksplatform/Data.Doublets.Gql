using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema
{
    class OrderByEnum : EnumerationGraphType<order_by>
    {
        public OrderByEnum()
        {
            Name = "OrderByEnum";
        }
    }
}
