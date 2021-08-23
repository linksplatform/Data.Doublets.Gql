using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types
{
    internal class LinksAggregateBigIntFieldsType : ObjectGraphType
    {
        public LinksAggregateBigIntFieldsType()
        {
            Field<LongGraphType>("id");
            Field<LongGraphType>("from_id");
            Field<LongGraphType>("to_id");
            Field<LongGraphType>("type_id");
        }
    }
}
