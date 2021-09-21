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
        }
    }
}
