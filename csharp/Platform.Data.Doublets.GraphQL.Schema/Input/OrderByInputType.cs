using Gql.Samples.Schemas.Link;
using GraphQL.Types;

namespace Input
{
    class OrderByInputType : InputObjectGraphType<OrderBy>
    {
        public OrderByInputType()
        {
            Field<OrderByInputType>("from");
            Field(x => x.from_id,nullable: true,type: typeof(OrderByEnum));
            Field(x => x.id, nullable: true, type: typeof(OrderByEnum));
            Field<OrderByInputType>("to");
            Field(x => x.to_id, nullable: true, type: typeof(OrderByEnum));
            Field<OrderByInputType>("type");
            Field(x => x.type_id, nullable: true, type: typeof(OrderByEnum));
        }
    }
}