using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types
{
    internal class LinksAggregateBigIntFieldsType : ObjectGraphType
    {
        public LinksAggregateBigIntFieldsType()
        {
            Field<LongGraphType>("id",null,true,null);
            Field<LongGraphType>("from_id", null, true, null);
            Field<LongGraphType>("to_id", null, true, null);
            Field<LongGraphType>("type_id", null, true, null);
        }
    }
}
