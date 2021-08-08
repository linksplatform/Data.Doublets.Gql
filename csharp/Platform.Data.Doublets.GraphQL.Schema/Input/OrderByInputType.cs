using GraphQL.Samples.Schemas.Link;
using GraphQL.Types;

namespace Input
{
    class OrderByInputType : InputObjectGraphType<OrderByEnum>
    {

        public OrderByInputType()
        {
            //Field<OrderByEnum>("order_by");
            Field<OrderByInputType>("from");
            Field<OrderByEnum>(x => x.from_id,nullable: true,type: typeof(OrderByEnum));
            Field<OrderByEnum>(x => x.id, nullable: true, type: typeof(OrderByEnum));
            Field<OrderByInputType>("to");
            Field<OrderByEnum>(x => x.to_id, nullable: true, type: typeof(OrderByEnum));
            Field<OrderByInputType>("type");
            Field<OrderByEnum>(x => x.type_id, nullable: true, type: typeof(OrderByEnum));
        }
    }
}