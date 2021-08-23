using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    internal class LinksFieldsOrderByInputType : InputObjectGraphType
    {
        public LinksFieldsOrderByInputType()
        {
            Field<OrderByInputType>("id");
            Field<OrderByInputType>("from_id");
            Field<OrderByInputType>("to_id");
            Field<OrderByInputType>("type_id");
        }
    }
}
