using GraphQL.Samples.Schemas.Link;
using GraphQL.Types;

namespace Input
{
    class OrderByInputType : InputObjectGraphType
    {
        public OrderByInputType()
        {
            //Field<OrderByEnum>("order_by");
            Field<OrderByInputType>("from");
            Field<OrderByEnum>("from_id");
            Field<OrderByEnum>("id");
            Field<OrderByInputType>("to");
            Field<OrderByEnum>("to_id");
            Field<OrderByInputType>("type");
            Field<OrderByEnum>("type_id");
        }
    }
}