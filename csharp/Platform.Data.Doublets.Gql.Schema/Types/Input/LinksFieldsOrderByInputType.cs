using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    internal class LinksFieldsOrderByInputType : InputObjectGraphType
    {
        public LinksFieldsOrderByInputType()
        {
            Field<OrderByEnumType>("id");
            Field<OrderByEnumType>("from_id");
            Field<OrderByEnumType>("to_id");
            Field<OrderByEnumType>("type_id");
        }
    }
}
