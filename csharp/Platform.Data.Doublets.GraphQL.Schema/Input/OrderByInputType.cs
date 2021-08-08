using GraphQL.Types;

namespace Input
{
    class OrderByInputType : InputObjectGraphType
    {
        public OrderByInputType()
        {
            Field<EnumerationGraphType>("order_by");
            Field<OrderByInputType>("from");
            Field<EnumerationGraphType>("from_id");
            Field<EnumerationGraphType>("id");
            Field<OrderByInputType>("to");
            Field<EnumerationGraphType>("to_id");
            Field<OrderByInputType>("type");
            Field<EnumerationGraphType>("type_id");
        }
    }
}