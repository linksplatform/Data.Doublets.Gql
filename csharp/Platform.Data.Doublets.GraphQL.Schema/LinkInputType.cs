using GraphQL.Types;

namespace GraphQL.Samples.Schemas.Link
{
    class LinkInputType : InputObjectGraphType
    {
        public LinkInputType()
        {
            Field<LongGraphType>("id");
            Field<LinkType>("from");
            Field<LongGraphType>("from_id");
            Field<LinkType>("to");
            Field<LongGraphType>("to_id");
            Field<LinkType>("type");
            Field<LongGraphType>("type_id");
        }
    }
}
