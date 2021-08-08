using GraphQL.Types;

namespace Input
{
    class OrderByInputType : InputObjectGraphType
    {
        public enum order_by
        {
            asc,
            asc_nulls_first,
            asc_nulls_last,
            desc,
            desc_nulls_first,
            desc_nulls_lasr
        }
        public OrderByInputType()
        {
            //Field<EnumerationGraphType<order_by>>("order_by");
            Field<OrderByInputType>("from");
            Field<EnumerationGraphType<order_by>>("from_id");
            Field<EnumerationGraphType<order_by>>("id");
            Field<OrderByInputType>("to");
            Field<EnumerationGraphType<order_by>>("to_id");
            Field<OrderByInputType>("type");
            Field<EnumerationGraphType<order_by>>("type_id");
        }
    }
}