using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    using MappedType = LinksSet;
    public class LinksSetInputType : InputObjectGraphType<MappedType>
    {
        public LinksSetInputType()
        {
            Name = "links_set_input";
            Field<LongGraphType>(nameof(MappedType.from_id));
            Field<LongGraphType>(nameof(MappedType.id));
            Field<LongGraphType>(nameof(MappedType.to_id));
            Field<LongGraphType>(nameof(MappedType.type_id));
        }
    }
}