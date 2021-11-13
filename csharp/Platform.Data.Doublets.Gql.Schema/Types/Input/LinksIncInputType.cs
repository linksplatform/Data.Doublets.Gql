using GraphQL.Types;

namespace Platform.Data.Doublets.Gql.Schema.Types.Input
{
    using MappedType = Links;

    internal class LinksIncInputType : InputObjectGraphType<MappedType>
    {
        public LinksIncInputType()
        {
            Name = "links_inc_input";
            Field<LongGraphType>(nameof(MappedType.id));
            Field<LongGraphType>(nameof(MappedType.from_id));
            Field<LongGraphType>(nameof(MappedType.to_id));
            Field<LongGraphType>(nameof(MappedType.type_id));
        }
    }
}
