using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    internal class LinksFieldsOrderByInputType : InputObjectGraphType
    {
        public LinksFieldsOrderByInputType()
        {
            Field<OrderByEnumType>("id", null, true, null);
            Field<OrderByEnumType>("from_id", null, true, null);
            Field<OrderByEnumType>("to_id", null, true, null);
            Field<OrderByEnumType>("type_id", null, true, null);
        }
    }
}
