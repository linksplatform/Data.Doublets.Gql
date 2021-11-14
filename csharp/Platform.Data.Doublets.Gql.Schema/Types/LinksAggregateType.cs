using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types
{
    using MappedType = LinksAggregate;
    public class LinksAggregateType : ObjectGraphType<MappedType>
    {
        public LinksAggregateType()
        {
            Name = "links_aggregate";
            Field<LinksAggregateFieldsType>(nameof(MappedType.aggregate));
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<LinksType>>>>("nodes");
        }
    }
}