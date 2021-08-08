using GraphQL.Types;

namespace Input
{
    class LinkInputType : InputObjectGraphType
    {
        public LinkInputType()
        {
            Field<LongGraphType>("id");
            Field<LongGraphType>("from_id");
            Field<LongGraphType>("to_id");
            Field<LongGraphType>("type_id");
        }
    }
}
