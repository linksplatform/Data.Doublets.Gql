using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types
{
    internal class LinksAggregateFloatFieldsType : ObjectGraphType<LinksAggregateFloatFields>
    {
        public LinksAggregateFloatFieldsType()
        {
            Field(x => x.id, nullable: true, type: typeof(FloatGraphType));
            Field(x => x.from_id, nullable: true, type: typeof(FloatGraphType));
            Field(x => x.to_id, nullable: true, type: typeof(FloatGraphType));
            Field(x => x.type_id, nullable: true, type: typeof(FloatGraphType));
        }
    }
}
