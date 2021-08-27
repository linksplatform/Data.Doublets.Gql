using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types
{
    internal class LinksAggregateFloatFieldsType : ObjectGraphType
    {
        public LinksAggregateFloatFieldsType()
        {
            Field<FloatGraphType>("id", null, true, null);
            Field<FloatGraphType>("from_id", null, true, null);
            Field<FloatGraphType>("to_id", null, true, null);
            Field<FloatGraphType>("type_id", null, true, null);
        }
    }
}
