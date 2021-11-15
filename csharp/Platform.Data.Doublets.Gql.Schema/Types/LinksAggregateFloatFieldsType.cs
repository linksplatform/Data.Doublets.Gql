using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types
{
    public class LinksAggregateFloatFieldsType : ObjectGraphType<LinksAggregateFloatFields>
    {
        public LinksAggregateFloatFieldsType()
        {
        }

        public LinksAggregateFloatFieldsType(string name)
        {
            Name = name;
            Field<FloatGraphType>(nameof(MappedType.from_id));
            Field<FloatGraphType>(nameof(MappedType.id));
            Field<FloatGraphType>(nameof(MappedType.to_id));
            Field<FloatGraphType>(nameof(MappedType.type_id));
        }
    }
}
