using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    internal class LinksFieldsOrderByInputType : InputObjectGraphType
    {
        public LinksFieldsOrderByInputType()
        {
            Field<LinksOrderByInputType>("id");
            Field<LinksOrderByInputType>("from_id");
            Field<LinksOrderByInputType>("to_id");
            Field<LinksOrderByInputType>("type_id");
        }
    }
}
